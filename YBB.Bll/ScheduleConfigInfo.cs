using System;
using System.Xml.Serialization;
using YBB.Bll.ScheduledEvents;

namespace YBB.Bll
{
    [Serializable]
    public class ScheduleConfigInfo : IConfigInfo
    {
        [XmlArray("events")]
        public Event[] Events;
    }

}
