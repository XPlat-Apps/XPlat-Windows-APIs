namespace XPlat.API.Threading.Tasks
{
    using System;
    using System.Threading.Tasks;

    public class TimeoutTask
    {
        private readonly TimeSpan timeout;

        private readonly Action timeoutAction;

        private bool taskCancelled;

        public TimeoutTask(TimeSpan timeout, Action timeoutAction)
        {
            this.timeout = timeout;
            this.timeoutAction = timeoutAction;

            Task.Factory.StartNew(this.OnTimeout, TaskCreationOptions.LongRunning);
        }

        public void Cancel()
        {
            this.taskCancelled = true;
        }

        private void OnTimeout()
        {
            var start = DateTime.UtcNow;
            while (!this.taskCancelled)
            {
                if (DateTime.UtcNow - start < this.timeout)
                {
                    Task.Delay(1).Wait();
                    continue;
                }

                this.timeoutAction?.Invoke();
                return;
            }
        }
    }
}