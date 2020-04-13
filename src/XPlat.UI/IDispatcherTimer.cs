namespace XPlat.UI
{
    using System;

    /// <summary>Provides a timer that is integrated into the Dispatcher queue, which is processed at a specified interval of time and at a specified priority.</summary>
    public interface IDispatcherTimer
    {
        /// <summary>Occurs when the timer interval has elapsed.</summary>
        event EventHandler<object> Tick;

        /// <summary>Gets or sets the amount of time between timer ticks.</summary>
        TimeSpan Interval { get; set; }

        /// <summary>Gets a value indicating whether the timer is running.</summary>
        bool IsEnabled { get; }

        /// <summary>Starts the DispatcherTimer.</summary>
        void Start();

        /// <summary>Stops the DispatcherTimer.</summary>
        void Stop();
    }
}