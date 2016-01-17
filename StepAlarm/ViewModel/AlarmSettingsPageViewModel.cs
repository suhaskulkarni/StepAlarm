using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StepAlarmPersistence.Persistence;
using StepCounterWrapper.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace StepAlarm.ViewModel
{
    public class AlarmSettingsPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AlarmSettingsPageViewModel()
        {
            SnoozeTime = new ObservableCollection<uint>();
            SnoozeNumber = new uint();
            SnoozeNumber = 1;

            StepsNumber = new uint();
            StepsNumber = 1;

            //DatabaseAccess dbAccess = new DatabaseAccess();

            ResourceContentLoader = new ResourceLoader();
            RepeatAlarm = ResourceContentLoader.GetString("RepeatOnce");
            SnoozeTimeCommand = new RelayCommand(PopulateSnoozeTime);
            RepeatAlarmCommand = new RelayCommand(PopulateRepeatAlarm);
            NumberOfStepsCommand = new RelayCommand(PopulateStepsList);
        }

        /// <summary>
        /// Snooze time button command.
        /// </summary>
        public RelayCommand SnoozeTimeCommand { get; set; }

        /// <summary>
        /// Repeat Alarm command
        /// </summary>
        public RelayCommand RepeatAlarmCommand { get; set; }

        /// <summary>
        /// Number of steps command
        /// </summary>
        public RelayCommand NumberOfStepsCommand { get; set; }

        /// <summary>
        /// Snooze number.
        /// </summary>
        private uint _snoozeNumber;

        /// <summary>
        /// Steps number.
        /// </summary>
        private uint _stepsNumber;

        /// <summary>
        /// Steps list.
        /// </summary>
        private ObservableCollection<uint> _stepsList;

        /// <summary>
        /// Snooze time.
        /// </summary>
        private ObservableCollection<uint> _snoozeTime;

        /// <summary>
        /// Repeat Alarm.
        /// </summary>
        private ObservableCollection<string> _repeatAlarmInDays;

        /// <summary>
        /// Step alarm toggle button.
        /// </summary>
        private bool _isStepAlarmEnabled;

        public ResourceLoader ResourceContentLoader { get; set; }

        /// <summary>
        /// Snooze Time binded.
        /// </summary>
        public ObservableCollection<uint> SnoozeTime
        {
            get
            {
                return _snoozeTime;
            }
            set
            { 
                Set("SnoozeTime", ref _snoozeTime, value, true);
            }
        }

                /// <summary>
        /// Snooze Time binded.
        /// </summary>
        public ObservableCollection<uint> StepsList
        {
            get
            {
                return _stepsList;
            }
            set
            {
                Set("StepsList", ref _stepsList, value, true);
            }
        }
        

        /// <summary>
        /// Repeat alarm private member.
        /// </summary>
        public string _repeatAlarm;

        /// <summary>
        /// Repeat Alarm
        /// </summary>
        public string RepeatAlarm
        {
            get
            {
                return _repeatAlarm;
            }
            set
            {
                Set("RepeatAlarm", ref _repeatAlarm, value, true);
            }
        }

        /// <summary>
        /// Repeat Alarm.
        /// </summary>
        public ObservableCollection<string> RepeatAlarmInDays
        {
            get
            {
                return _repeatAlarmInDays;
            }
            set
            {
                Set("RepeatAlarmInDays", ref _repeatAlarmInDays, value, true);
            }
        }

        /// <summary>
        /// Snooze number integer.
        /// </summary>
        public uint SnoozeNumber
        {
            get
            {
                return _snoozeNumber;
            }
            set
            {
                Set("SnoozeNumber", ref _snoozeNumber, value, true);
            }
        }

        /// <summary>
        /// Steps number.
        /// </summary>
        public uint StepsNumber
        {
            get
            {
                return _stepsNumber;
            }
            set
            {
                Set("StepsNumber", ref _stepsNumber, value, true);
            }
        }

        public bool IsStepAlarmEnabled
        {
            get
            {
                return _isStepAlarmEnabled;
            }
            set
            {
                Set("IsStepAlarmEnabled", ref _isStepAlarmEnabled, value, true);
            }
        }

        /// <summary>
        /// Populates Snooze time flyout.
        /// </summary>
        public void PopulateSnoozeTime()
        {
            SnoozeTime = new ObservableCollection<uint>();
            for(int i = 1; i <=60; i++)
            {
                SnoozeTime.Add(Convert.ToUInt32(i));
            }
        }

        /// <summary>
        /// Populates Steps list flyout.
        /// </summary>
        public void PopulateStepsList()
        {
            StepsList = new ObservableCollection<uint>();
            for(int i = 1; i <= 100; i++)
            {
                StepsList.Add(Convert.ToUInt32(i));
            }
        }

        /// <summary>
        /// Populates repeat days flyout.
        /// </summary>
        public void PopulateRepeatAlarm()
        {
            RepeatAlarmInDays = new ObservableCollection<string>();
            foreach (var item in Enum.GetValues(typeof(DaysInWeek)))
            {
                RepeatAlarmInDays.Add(ResourceContentLoader.GetString(item.ToString()));
            }
        }

    }

}
