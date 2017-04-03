namespace XPlat.UI.Core
{
    using System.Threading.Tasks;

    public interface ICoreDispatcher
    {
        /// <summary>Schedules the provided callback on the UI thread from a worker thread.</summary>
        /// <param name="callback">The callback on which the dispatcher returns when the event is dispatched.</param>
        void Run(DispatchedHandler callback);

        /// <summary>Schedules the provided callback on the UI thread from a worker thread, and returns the results asynchronously.</summary>
        /// <returns>The object that provides handlers for the completed async event dispatch.</returns>
        /// <param name="callback">The callback on which the dispatcher returns when the event is dispatched.</param>
        Task RunAsync(DispatchedHandler callback);
    }
}