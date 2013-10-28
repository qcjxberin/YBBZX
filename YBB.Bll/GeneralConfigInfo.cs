using System;
using System.Runtime.CompilerServices;

namespace YBB.Bll
{
    [Serializable]
    public class GeneralConfigInfo : IConfigInfo
    {
        private int int_0 = 1;
        private int int_1 = 10;
        private int int_2;
        private int int_3;
        private int int_4 = 1;
        private int int_5;
        [CompilerGenerated]
        private int int_6;
        private string string_0 = "";
        private string string_1 = "";
        [CompilerGenerated]
        private string string_10;
        [CompilerGenerated]
        private string string_11;
        [CompilerGenerated]
        private string string_12;
        [CompilerGenerated]
        private string string_13;
        [CompilerGenerated]
        private string string_14;
        [CompilerGenerated]
        private string string_15;
        private string string_2 = "";
        private string string_3 = "";
        private string string_4 = "";
        private string string_5 = "";
        private string string_6 = "";
        private string string_7;
        private string string_8;
        private string string_9;

        public string AppKey
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = value;
            }
        }

        public string AppSecret
        {
            get
            {
                return this.string_7;
            }
            set
            {
                this.string_7 = value;
            }
        }

        public string AuthorizeURL
        {
            get
            {
                return this.string_9;
            }
            set
            {
                this.string_9 = value;
            }
        }

        public string BackgroundColor
        {
            [CompilerGenerated]
            get
            {
                return this.string_10;
            }
            [CompilerGenerated]
            set
            {
                this.string_10 = value;
            }
        }

        public string BackgroundColorText
        {
            [CompilerGenerated]
            get
            {
                return this.string_11;
            }
            [CompilerGenerated]
            set
            {
                this.string_11 = value;
            }
        }

        public string BackgroundGuding
        {
            [CompilerGenerated]
            get
            {
                return this.string_12;
            }
            [CompilerGenerated]
            set
            {
                this.string_12 = value;
            }
        }

        public string BackgroundLogin
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

        public string BackgroundLoginHeight
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string BackgroundPosition
        {
            [CompilerGenerated]
            get
            {
                return this.string_14;
            }
            [CompilerGenerated]
            set
            {
                this.string_14 = value;
            }
        }

        public string BackgroundPositionLeft
        {
            [CompilerGenerated]
            get
            {
                return this.string_15;
            }
            [CompilerGenerated]
            set
            {
                this.string_15 = value;
            }
        }

        public string BackgroundPositionTop
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

        public string BackgroundRepeat
        {
            [CompilerGenerated]
            get
            {
                return this.string_13;
            }
            [CompilerGenerated]
            set
            {
                this.string_13 = value;
            }
        }

        public int BackgroundTop
        {
            [CompilerGenerated]
            get
            {
                return this.int_6;
            }
            [CompilerGenerated]
            set
            {
                this.int_6 = value;
            }
        }

        public int BaiboApi
        {
            get
            {
                return this.int_5;
            }
            set
            {
                this.int_5 = value;
            }
        }

        public string CallBackURI
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = value;
            }
        }

        public int Installation
        {
            get
            {
                return this.int_4;
            }
            set
            {
                this.int_4 = value;
            }
        }

        public int TongClose
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

        public string TongFilepath1
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public string TongFilepath2
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public int TongHeight1
        {
            get
            {
                return this.int_2;
            }
            set
            {
                this.int_2 = value;
            }
        }

        public int TongHeight2
        {
            get
            {
                return this.int_3;
            }
            set
            {
                this.int_3 = value;
            }
        }

        public int TongTime
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

        public string TongUrl
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }
    }

}
