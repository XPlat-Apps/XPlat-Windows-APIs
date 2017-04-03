namespace XPlat.UI.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;

    public sealed class CoreDispatcher : ICoreDispatcher
    {
        private static readonly Lazy<ICoreDispatcher> CurrentDispatcher =
            new Lazy<ICoreDispatcher>(() => new CoreDispatcher(), LazyThreadSafetyMode.PublicationOnly);

        private readonly Windows.UI.Core.CoreDispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreDispatcher"/> class.
        /// </summary>
        public CoreDispatcher()
        {
            this.dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
        }

        public static ICoreDispatcher Current => CurrentDispatcher.Value;

        /// <inheritdoc />
        public void Run(DispatchedHandler callback)
        {
            this.dispatcher?.RunAsync(CoreDispatcherPriority.Normal, () => callback());
        }

        /// <inheritdoc />
        public Task RunAsync(DispatchedHandler callback)
        {
            return this.dispatcher?.RunAsync(CoreDispatcherPriority.Normal, () => callback()).AsTask();
        }
    }
}