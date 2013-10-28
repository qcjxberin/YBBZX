using System;
using System.Threading;

namespace YBB.Bll
{
    public class Semaphore
    {
        private int int_0;
        private object object_0;

        public Semaphore()
            : this(1)
        {
        }

        public Semaphore(int int_1)
        {
            this.object_0 = new object();
            if (int_1 < 0)
            {
                throw new ArgumentException("Semaphore must have a count of at least 0.", "count");
            }
            this.int_0 = int_1;
        }

        public void AddOne()
        {
            this.V();
        }

        public void P()
        {
            lock (this.object_0)
            {
                while (this.int_0 <= 0)
                {
                    Monitor.Wait(this.object_0, -1);
                }
                this.int_0--;
            }
        }

        public void Reset(int int_1)
        {
            lock (this.object_0)
            {
                this.int_0 = int_1;
            }
        }

        public void V()
        {
            lock (this.object_0)
            {
                this.int_0++;
                Monitor.Pulse(this.object_0);
            }
        }

        public void WaitOne()
        {
            this.P();
        }
    }

}
