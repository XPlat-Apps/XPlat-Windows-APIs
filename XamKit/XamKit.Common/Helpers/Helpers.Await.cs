namespace XamKit
{
    using System.Threading;
    using System.Threading.Tasks;

    using XamKit.Threading.Tasks;

    public partial class Helpers
    {
        public static TaskSchedulerAwaiter CreateNewTaskSchedulerAwaiter()
        {
            return new TaskSchedulerAwaiter(
                SynchronizationContext.Current != null ? TaskScheduler.Default : null);
        }
    }
}