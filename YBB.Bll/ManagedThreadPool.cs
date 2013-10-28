using System;
using System.Collections;
using System.Threading;

namespace YBB.Bll
{
    public class ManagedThreadPool
    {
        private static int _inUseThreads;
        private const int _maxWorkerThreads = 10;
        private static object _poolLock;
        private static Queue _waitingCallbacks;
        private static Semaphore _workerThreadNeeded;
        private static ArrayList _workerThreads;

        static ManagedThreadPool()
        {
            old_acctor_mc();
        }

        private static void Initialize()
        {
            _waitingCallbacks = new Queue();
            _workerThreads = new ArrayList();
            _inUseThreads = 0;
            _workerThreadNeeded = new Semaphore(0);
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(new ThreadStart(ManagedThreadPool.ProcessQueuedItems));
                _workerThreads.Add(thread);
                thread.Name = "ManagedPoolThread #" + i.ToString();
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private static void old_acctor_mc()
        {
            _poolLock = new object();
            Initialize();
        }

        private static void ProcessQueuedItems()
        {
            while (true)
            {
                _workerThreadNeeded.WaitOne();
                Class4 class2 = null;
                lock (_poolLock)
                {
                    if (_waitingCallbacks.Count > 0)
                    {
                        try
                        {
                            class2 = (Class4)_waitingCallbacks.Dequeue();
                        }
                        catch
                        {
                        }
                    }
                }
                if (class2 != null)
                {
                    try
                    {
                        Interlocked.Increment(ref _inUseThreads);
                        class2.Callback(class2.State);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        Interlocked.Decrement(ref _inUseThreads);
                    }
                }
            }
        }

        public static void QueueUserWorkItem(WaitCallback waitCallback_0)
        {
            QueueUserWorkItem(waitCallback_0, null);
        }

        public static void QueueUserWorkItem(WaitCallback waitCallback_0, object object_0)
        {
            Class4 class2 = new Class4(waitCallback_0, object_0);
            lock (_poolLock)
            {
                _waitingCallbacks.Enqueue(class2);
            }
            _workerThreadNeeded.AddOne();
        }

        public static void Reset()
        {
            lock (_poolLock)
            {
                try
                {
                    foreach (object obj2 in _waitingCallbacks)
                    {
                        Class4 class2 = (Class4)obj2;
                        if (class2.State is IDisposable)
                        {
                            ((IDisposable)class2.State).Dispose();
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    foreach (Thread thread in _workerThreads)
                    {
                        if (thread != null)
                        {
                            thread.Abort("reset");
                        }
                    }
                }
                catch
                {
                }
                Initialize();
            }
        }

        public static int ActiveThreads
        {
            get
            {
                return _inUseThreads;
            }
        }

        public static int MaxThreads
        {
            get
            {
                return 10;
            }
        }

        public static int WaitingCallbacks
        {
            get
            {
                lock (_poolLock)
                {
                    return _waitingCallbacks.Count;
                }
            }
        }

        private class Class4
        {
            private object object_0;
            private WaitCallback waitCallback_0;

            public Class4(WaitCallback waitCallback_1, object object_1)
            {
                this.waitCallback_0 = waitCallback_1;
                this.object_0 = object_1;
            }

            public WaitCallback Callback
            {
                get
                {
                    return this.waitCallback_0;
                }
            }

            public object State
            {
                get
                {
                    return this.object_0;
                }
            }
        }
    }

}
