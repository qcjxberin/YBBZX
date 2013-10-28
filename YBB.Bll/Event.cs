using System;
using System.Xml.Serialization;

namespace YBB.Bll
{
    [Serializable, XmlRoot("event")]
    public class Event
    {
        private bool bool_0;
        private int int_0 = -1;
        private int int_1 = 60;
        private string string_0;
        private string string_1;

        [XmlAttribute("enabled")]
        public bool Enabled
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        [XmlAttribute("minutes")]
        public int Minutes
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        [XmlAttribute("name")]
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
