namespace XPlat.UI.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Foundation;

    public sealed class CoreDispatcher : ICoreDispatcher
    {
        private static readonly Lazy<ICoreDispatcher> CurrentDispatcher =
            new Lazy<ICoreDispatcher>(() => new CoreDispatcher(), LazyThreadSafetyMode.PublicationOnly);

        private NSObject obj;

        private NSObject Obj => this.obj ?? (this.obj = new NSObject());

        public static ICoreDispatcher Current => CurrentDispatcher.Value;

        /// <inheritdoc />
        public void Run(DispatchedHandler callback)
        {
            if (NSThread.Current.IsMainThread)
            {
                callback();
                return;
            }

            this.Obj.BeginInvokeOnMainThread(() => callback());
        }

        /// <inheritdoc />
        public Task RunAsync(DispatchedHandler callback)
        {
            return Task.Run(
                () =>
                    {
                        this.Obj.BeginInvokeOnMainThread(() => callback());
                    });
        }
    }
}