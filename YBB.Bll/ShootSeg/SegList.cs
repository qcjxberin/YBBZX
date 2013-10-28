using System.Collections;

namespace YBB.Bll.ShootSeg
{
    public class SegList
    {
        private ArrayList arrayList_0 = new ArrayList();
        public int MaxLength = 0;

        public void Add(object object_0)
        {
            this.arrayList_0.Add(object_0);
            if (this.MaxLength < object_0.ToString().Length)
            {
                this.MaxLength = object_0.ToString().Length;
            }
        }

        public bool Contains(object object_0)
        {
            return this.arrayList_0.Contains(object_0);
        }

        public object GetElem(int int_0)
        {
            if (int_0 < this.Count)
            {
                return this.arrayList_0[int_0];
            }
            return null;
        }

        public void SetElem(int int_0, object object_0)
        {
            this.arrayList_0[int_0] = object_0;
        }

        public void Sort()
        {
            this.Sort(this);
        }

        public void Sort(SegList segList_0)
        {
            int num = 0;
            for (int i = 0; i < (segList_0.Count - 1); i++)
            {
                num = i;
                for (int j = i + 1; j < segList_0.Count; j++)
                {
                    int length;
                    int num5;
                    string str = segList_0.GetElem(j).ToString();
                    string str2 = segList_0.GetElem(num).ToString();
                    if (str == "null")
                    {
                        length = 0;
                    }
                    else
                    {
                        length = str.Length;
                    }
                    if (str2 == "null")
                    {
                        num5 = 0;
                    }
                    else
                    {
                        num5 = str2.Length;
                    }
                    if (length > num5)
                    {
                        num = j;
                    }
                }
                object elem = segList_0.GetElem(num);
                segList_0.SetElem(num, segList_0.GetElem(i));
                segList_0.SetElem(i, elem);
            }
        }

        public int Count
        {
            get
            {
                return this.arrayList_0.Count;
            }
        }
    }

 

}
