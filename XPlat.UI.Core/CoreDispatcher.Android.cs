#if __ANDROID__
namespace XPlat.UI.Core
{
    using System.Threading.Tasks;

    using Android.App;

    /// <summary>Provides the core event message dispatcher. Instances of this type are responsible for processing messages and dispatching the events to the UI thread.</summary>
    public sealed class CoreDispatcher : ICoreDispatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreDispatcher"/> class.
        /// </summary>
        public CoreDispatcher(Activity activity)
        {
            this.Activity = activity;
        }

        public Activity Activity { get; set; }

        /// <summary>Schedules the provided callback on the UI thread from a worker thread without waiting for a result.</summary>
        /// <param name="agileCallback">The callback on which the dispatcher returns when the event is dispatched.</param>
        public void Run(DispatchedHandler agileCallback)
        {
            this.Run(CoreDispatcherPriority.Normal, agileCallback);
        }

        /// <summary>Schedules the provided callback on the UI thread from a worker thread without waiting for a result.</summary>
        /// <param name="priority">Specifies the priority for event dispatch.</param>
        /// <param name="agileCallback">The callback on which the dispatcher returns when the event is dispatched.</param>
        public void Run(CoreDispatcherPriority priority, DispatchedHandler agileCallback)
        {
            this.Activity.RunOnUiThread(() => agileCallback());
        }

        /// <summary>Schedules the provided callback on the UI thread from a worker thread, and returns the results asynchronously.</summary>
        /// <returns>The object that provides handlers for the completed async event dispatch.</returns>
        /// <param name="agileCallback">The callback on which the dispatcher returns when the event is dispatched.</param>
        public Task RunAsync(DispatchedHandler agileCallback)
        {
            return this.RunAsync(CoreDispatcherPriority.Normal, agileCallback);
        }

        /// <summary>Schedules the provided callback on the UI thread from a worker thread, and returns the results asynchronously.</summary>
        /// <returns>The object that provides handlers for the completed async event dispatch.</returns>
        /// <param name="priority">Specifies the priority for event dispatch.</param>
        /// <param name="agileCallback">The callback on which the dispatcher returns when the event is dispatched.</param>
        public Task RunAsync(CoreDispatcherPriority priority, DispatchedHandler agileCallback)
        {
            this.Run(priority, agileCallback);
            return Task.CompletedTask;
        }
    }
}
#endif