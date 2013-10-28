using System.Collections.Generic;
using System.Threading;

namespace YBB.Bll.ScheduledEvents
{
    public class EventManager
    {
        public static string RootPath;
        public static readonly int TimerMinutesInterval = 1;

        public static void Execute()
        {
            YBB.Bll.Event[] events = ScheduleConfigs.GetConfig().Events;
            List<Event> list = new List<Event>();
            foreach (var event2 in events)
            {
                if (event2.Enabled)
                {
                    Event item = new Event
                    {
                        Name = event2.Name,
                        Minutes = event2.Minutes,
                        ScheduleType = event2.ScheduleType,
                        TimeOfDay = event2.TimeOfDay
                    };
                    list.Add(item);
                }
            }
            Event[] eventArray2 = list.ToArray();
            Event event4 = null;
            if (eventArray2 != null)
            {
                for (int i = 0; i < eventArray2.Length; i++)
                {
                    event4 = eventArray2[i];
                    if (event4.ShouldExecute)
                    {
                        event4.UpdateTime();
                        IEvent iEventInstance = event4.IEventInstance;
                        ManagedThreadPool.QueueUserWorkItem(new WaitCallback(iEventInstance.Execute));
                    }
                }
            }
        }

    }

}
