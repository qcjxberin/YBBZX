using System;
using System.Xml.Serialization;

namespace YBB.Bll.ScheduledEvents
{
    public class Event
    {
        private bool bool_0;
        private DateTime dateTime_0;
        private IEvent ievent_0;
        private int int_0 = -1;
        private int int_1 = 60;
        private string string_0;
        private string string_1;

        private void method_0()
        {
            if (this.ievent_0 == null)
            {
                if (this.ScheduleType == null)
                {
                    EventLogs.WriteFailedLog("计划任务没有定义其 type 属性");
                }
                Type type = Type.GetType(this.ScheduleType);
                if (type == null)
                {
                    EventLogs.WriteFailedLog(string.Format("计划任务 {0} 无法被正确识别", this.ScheduleType));
                }
                else
                {
                    this.ievent_0 = (IEvent)Activator.CreateInstance(type);
                    if (this.ievent_0 == null)
                    {
                        EventLogs.WriteFailedLog(string.Format("计划任务 {0} 未能正确加载", this.ScheduleType));
                    }
                }
            }
        }

        public void UpdateTime()
        {
            this.LastCompleted = DateTime.Now;
            Events.SetLastExecuteScheduledEventDateTime(this.Name, Environment.MachineName, this.LastCompleted);
        }

        public IEvent IEventInstance
        {
            get
            {
                this.method_0();
                return this.ievent_0;
            }
        }

        [XmlIgnore]
        public DateTime LastCompleted
        {
            get
            {
                return this.dateTime_0;
            }
            set
            {
                this.bool_0 = true;
                this.dateTime_0 = value;
            }
        }

        public int Minutes
        {
            get
            {
                if (this.int_1 < EventManager.TimerMinutesInterval)
                {
                    return EventManager.TimerMinutesInterval;
                }
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public string Name
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        [XmlAttribute("type")]
        public string ScheduleType
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        [XmlIgnore]
        public bool ShouldExecute
        {
            get
            {
                if (!this.bool_0)
                {
                    this.LastCompleted = Events.GetLastExecuteScheduledEventDateTime(this.Name, Environment.MachineName);
                }
                if (this.TimeOfDay <= -1)
                {
                    return (this.LastCompleted.AddMinutes((double)this.Minutes) <= DateTime.Now);
                }
                DateTime now = DateTime.Now;
                DateTime time2 = new DateTime(now.Year, now.Month, now.Day);
                return ((this.LastCompleted < time2.AddMinutes((double)this.TimeOfDay)) && (time2.AddMinutes((double)this.TimeOfDay) <= DateTime.Now));
            }
        }

        [XmlAttribute("time_of_day")]
        public int TimeOfDay
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }
    }

}
