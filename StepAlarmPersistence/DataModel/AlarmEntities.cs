using SQLite;
using StepCounterWrapper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepAlarmPersistence.DataModel
{
    public class AlarmEntities
    {
        [PrimaryKey]
        /// <summary>
        /// Name of the alarm.
        /// </summary>
        public string AlarmLabel { get; set; }

        /// <summary>
        /// To set alarm time.
        /// </summary>
        public DateTime AlarmTime { get; set; }

        /// <summary>
        /// List of days to repeat the alarm.
        /// </summary>
        public List<DaysInWeek> AlarmRepeat { get; set; }

        /// <summary>
        /// Alarm music.
        /// </summary>
        public string AlarmSound { get; set; }

        /// <summary>
        /// Snooze duration.
        /// </summary>
        public uint SnoozeTime { get; set; }

        /// <summary>
        /// Sets or resets walk up.
        /// </summary>
        public bool IsWalkUpAlarmEnabled { get; set; }

        /// <summary>
        /// To set number of steps to walk for alarm to be disabled.
        /// </summary>
        public uint NoOfSteps { get; set; }
    }
}
