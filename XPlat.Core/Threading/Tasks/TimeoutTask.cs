// <copyright file="TimeoutTask.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Threading.Tasks
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a task that is actioned when the timeout occurs unless cancelled.
    /// </summary>
    public class TimeoutTask
    {
        private readonly TimeSpan timeout;

        private readonly Action timeoutAction;

        private bool taskCancelled;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeoutTask"/> class.
        /// </summary>
        /// <param name="timeout">
        /// The timeout perioud.
        /// </param>
        /// <param name="timeoutAction">
        /// The action to perform when the timeout occurs.
        /// </param>
        public TimeoutTask(TimeSpan timeout, Action timeoutAction)
        {
            this.timeout = timeout;
            this.timeoutAction = timeoutAction;

            Task.Factory.StartNew(this.OnTimeout, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Cancels the timeout task to prevent the timeout action from occurring.
        /// </summary>
        public void Cancel()
        {
            this.taskCancelled = true;
        }

        private void OnTimeout()
        {
            DateTime start = DateTime.UtcNow;
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