namespace XPlat.UI
{
    using System;
    using System.Threading;

    /// <summary>Provides a timer that is integrated into the Dispatcher queue, which is processed at a specified interval of time and at a specified priority.</summary>
    public class DispatcherTimer : IDispatcherTimer
    {
        private Timer timer;

#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherTimer"/> class with the CoreApplication.MainView.CoreWindow.Dispatcher.
        /// </summary>
        public DispatcherTimer() : this(Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherTimer"/> class.
        /// </summary>
        public DispatcherTimer(Windows.UI.Core.CoreDispatcher dispatcher)
        {
            this.Dispatcher = dispatcher;
        }

        public Windows.UI.Core.CoreDispatcher Dispatcher { get; set; }
#elif __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherTimer"/> class.
        /// </summary>
        public DispatcherTimer(UIKit.UIViewController viewController)
        {
            this.ViewController = viewController;
        }

        public UIKit.UIViewController ViewController { get; set; }
#elif __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherTimer"/> class.
        /// </summary>
        public DispatcherTimer(Android.App.Activity activity)
        {
            this.Activity = activity;
        }

        public Android.App.Activity Activity { get; set; }
#endif

        /// <summary>Occurs when the timer interval has elapsed.</summary>
        public event EventHandler<object> Tick;

        /// <summary>Gets or sets the amount of time between timer ticks.</summary>
        public TimeSpan Interval { get; set; } = Timeout.InfiniteTimeSpan;

        /// <summary>Gets a value indicating whether the timer is running.</summary>
        public bool IsEnabled { get; private set; }

        /// <summary>Starts the DispatcherTimer.</summary>
        public void Start()
        {
            if (this.timer == null)
            {
                this.timer = new Timer(
                    x => this.UpdateTick(),
                    null,
                    0,
                    (int)Math.Ceiling(this.Interval.TotalMilliseconds));
            }
            else
            {
                this.timer.Change(
                    (int)Math.Ceiling(TimeSpan.Zero.TotalMilliseconds),
                    (int)Math.Ceiling(this.Interval.TotalMilliseconds));
            }

            this.IsEnabled = true;
        }

        /// <summary>Stops the DispatcherTimer.</summary>
        public void Stop()
        {
            this.timer?.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            this.IsEnabled = false;
        }

        private void UpdateTick()
        {
#if __IOS__
            this.ViewController.BeginInvokeOnMainThread(() =>
            {
#elif __ANDROID__
            this.Activity.RunOnUiThread(() =>
            {
#elif WINDOWS_UWP
            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
#endif
                Tick?.Invoke(this, null);
#if __IOS__ || __ANDROID__ || WINDOWS_UWP
            });
#endif
        }
    }
}