#if WINDOWS_UWP
namespace XPlat.UI.Core
{
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using XPlat.UI.Core.Extensions;

    /// <summary>Provides the core event message dispatcher. Instances of this type are responsible for processing messages and dispatching the events to the UI thread.</summary>
    public sealed class CoreDispatcher : ICoreDispatcher
    {
        private readonly Windows.UI.Core.CoreDispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreDispatcher"/> class with the CoreApplication.MainView.CoreWindow.Dispatcher.
        /// </summary>
        public CoreDispatcher() : this(CoreApplication.MainView.CoreWindow.Dispatcher)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreDispatcher"/> class.
        /// </summary>
        public CoreDispatcher(Windows.UI.Core.CoreDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public static implicit operator CoreDispatcher(Windows.UI.Core.CoreDispatcher dispatcher)
        {
            return new CoreDispatcher(dispatcher);
        }

        public static implicit operator Windows.UI.Core.CoreDispatcher(CoreDispatcher dispatcher)
        {
            return dispatcher?.dispatcher;
        }

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
            this.RunAsync(priority, agileCallback);
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
            if (!this.dispatcher.HasThreadAccess)
            {
                return this.dispatcher?.RunAsync(priority.ToWindowsCoreDispatcherPriority(), () => agileCallback())
                    .AsTask();
            }

            agileCallback();
            return Task.CompletedTask;
        }
    }
}
#endif