namespace XPlat.UI.Core
{
    using System.Threading.Tasks;

    using Android.App;

    public sealed class CoreDispatcher : ICoreDispatcher
    {
        private readonly Activity activity;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreDispatcher"/> class.
        /// </summary>
        public CoreDispatcher(Activity activity)
        {
            this.activity = activity;
        }

        /// <inheritdoc />
        public void Run(DispatchedHandler callback)
        {
            this.activity.RunOnUiThread(() => callback());
        }

        /// <inheritdoc />
        public Task RunAsync(DispatchedHandler callback)
        {
            return Task.Run(
                () =>
                    {
                        this.activity.RunOnUiThread(() => callback());
                    });
        }
    }
}