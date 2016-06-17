namespace XamKit.Threading.Tasks
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public struct TaskSchedulerAwaiter : INotifyCompletion
    {
        private readonly TaskScheduler taskScheduler;

        internal TaskSchedulerAwaiter(TaskScheduler taskScheduler)
        {
            this.taskScheduler = taskScheduler;
        }

        public TaskSchedulerAwaiter GetAwaiter()
        {
            return this;
        }

        public bool IsCompleted => this.taskScheduler == null;

        public void OnCompleted(Action completedAction)
        {
            if (this.taskScheduler == null)
            {
                throw new InvalidOperationException("The task scheduler has been disposed.");
            }

            Task.Factory.StartNew(completedAction, CancellationToken.None, TaskCreationOptions.None, this.taskScheduler);
        }

        public void GetResult()
        {
        }
    }
}