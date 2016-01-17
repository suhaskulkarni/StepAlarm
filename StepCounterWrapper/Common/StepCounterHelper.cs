using Lumia.Sense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StepCounterWrapper.Common
{
    public sealed class StepCounterHelper
    {
        ///<summary>
        ///Constructor
        ///</summary>
        public StepCounterHelper(int timeInterval)
        {
            _timer = new Timer(this.StepsChangedTimerCallBack, null, timeInterval, Timeout.Infinite);
        }

        /// <summary>
        /// The timer.
        /// </summary>
        Timer _timer { get; set; }

        /// <summary>
        /// Current Number of steps
        /// </summary>
        StepCounterReading _stepsReading { get; set; }

        /// <summary>
        /// Previous Number of steps
        /// </summary>
        StepCounterReading _previousStepsReading { get; set; }

        /// <summary>
        /// Sens error code
        /// </summary>
        SenseError _lastError { get; set; }

        /// <summary>
        /// Represents the method that will handle the <see cref="StepCounterHelper.StepsChanged"/>event.
        /// </summary>
        public delegate void StepsChangedEventHandler(object sender, StepsChangedEventArgs e);


        /// <summary>
        ///Registers the event of the steps that is changed.
        /// </summary>
        public event StepsChangedEventHandler StepsChanged;

        /// <summary>
        /// Gets the total steps till now.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> GetStepsCounterAsync()
        {
            StepCounter stepCounter = null;
            try
            {
                _previousStepsReading = _stepsReading;
                stepCounter = await StepCounter.GetDefaultAsync();
                _stepsReading = await stepCounter.GetStepCountAtAsync(DateTime.Now.Date);
            }
            catch (Exception e)
            {
                _lastError = SenseHelper.GetSenseError(e.HResult);
                return false;
            }
            finally
            {
                if (stepCounter != null)
                    stepCounter.Dispose();
            }
            return true;
        }

        /// <summary>
        /// Call back for Steps changed.
        /// </summary>
        /// <param name="state"></param>
        private async void StepsChangedTimerCallBack(object state)
        {
            if (await GetStepsCounterAsync())
            {
                if (StepsChanged != null)
                {
                    this.StepsChanged(this, new StepsChangedEventArgs(Convert.ToInt32(_previousStepsReading.WalkingStepCount - _stepsReading.WalkingStepCount)));
                }
            }
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            if(_timer != null)
            {
                _timer.Dispose();
            }
        }        
    }

    /// <summary>
    /// Class used to hold the event data required when there is change in steps count.
    /// </summary>
    public class StepsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// StepsChangedEventArgs Constructor.
        /// </summary>
        /// <param name="StepsCount"></param>
        public StepsChangedEventArgs(int StepsCount)
        {
            this.DeltaStepsCount = StepsCount;
        }
        /// <summary>
        /// Change in steps count
        /// </summary>
        public int DeltaStepsCount { get; set; }
    }
}
