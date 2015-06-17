using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CodingPractice
{
    public interface ICPUTask
    {
        void Execute();
    }
    public interface ITaskScheduler
    {
        void Schedule(ICPUTask task);
        void Stop();
        void Pause();
        void Resume();
        void Configure(int numThreads);
    }
    public class TaskScheduler:ITaskScheduler
    {

        public void Schedule(ICPUTask task)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Configure(int numThreads)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }
        public void Resume()
        {
            throw new NotImplementedException();
        }
        
    }

    internal class SharedResourceQueue
    {
        private readonly ConcurrentQueue<ICPUTask> queue;
        private readonly ManualResetEvent itemAvailableWaitHandle;
        private readonly ManualResetEvent itemFinishedWaitHandle;
        private readonly ManualResetEvent blockWaitHandle;
        private volatile bool shouldBlockConsumers;
        private int scheduledJobs;
 
        public SharedResourceQueue()
        {
            queue = new ConcurrentQueue<ICPUTask>();
            itemAvailableWaitHandle = new ManualResetEvent(false);
            itemFinishedWaitHandle = new ManualResetEvent(false);
            blockWaitHandle = new ManualResetEvent(false);
        }

        public void Enqueue(ICPUTask task)
        {
            queue.Enqueue(task);
            Interlocked.Increment(ref scheduledJobs);
            itemAvailableWaitHandle.Set();
        }

        public ICPUTask TryDequeue()
        {
            if (shouldBlockConsumers)
                blockWaitHandle.WaitOne();

            ICPUTask task;
            if(queue.TryDequeue(out task))
            {
                return task;
            }
            itemAvailableWaitHandle.Reset();
            itemAvailableWaitHandle.WaitOne();
            return null;
        }

        public void SetTaskDone()
        {
            Interlocked.Decrement(ref scheduledJobs);
            itemFinishedWaitHandle.Set();
        }

        public void WaitForQueue()
        {
            while(scheduledJobs > 0)
            {
                itemFinishedWaitHandle.WaitOne();
            }
        }

        public List<ICPUTask> TakeAll()
        {
            scheduledJobs = 0;

            scheduledJobs = 0;
            return new List<ICPUTask>(queue.ToArray());
        }

        public void NotifyConsumers()
        {
            itemAvailableWaitHandle.Set();
        }

        public void BlockConsumers()
        {
            blockWaitHandle.Reset();
            shouldBlockConsumers = true;
        }
    }
}
