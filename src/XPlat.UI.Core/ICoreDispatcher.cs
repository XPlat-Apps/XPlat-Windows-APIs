namespace XPlat.UI.Core
{
    using System.Threading.Tasks;

    /// <summary>Provides the core event message dispatcher. Instances of this type are responsible for processing messages and dispatching the events to the UI thread.</summary>
    public interface ICoreDispatcher
    {
        /// <summary>Schedules the provided callback on the UI thread from a worker thread without waiting for a result.</summary>
        /// <param name="agileCallback">The callback on which the dispatcher returns when the event is dispatched.</param>
        void Run(DispatchedHandler agileCallback);

        /// <summary>Schedules the provided callback on the UI thread from a worker thread without waiting for a result.</summary>
        /// <param name="priority">Specifies the priority for event dispatch.</param>
        /// <param name="agileCallback">The callback on which the dispatcher returns when the event is dispatched.</param>
        void Run(CoreDispatcherPriority priority, DispatchedHandler agileCallback);

        /// <summary>Schedules the provided callback on the UI thread from a worker thread, and returns the results asynchronously.</summary>
        /// <returns>The object that provides handlers for the completed async event dispatch.</returns>
        /// <param name="agileCallback">The callback on which the dispatcher returns when the event is dispatched.</param>
        Task RunAsync(DispatchedHandler agileCallback);

        /// <summary>Schedules the provided callback on the UI thread from a worker thread, and returns the results asynchronously.</summary>
        /// <returns>The object that provides handlers for the completed async event dispatch.</returns>
        /// <param name="priority">Specifies the priority for event dispatch.</param>
        /// <param name="agileCallback">The callback on which the dispatcher returns when the event is dispatched.</param>
        Task RunAsync(CoreDispatcherPriority priority, DispatchedHandler agileCallback);
    }
}