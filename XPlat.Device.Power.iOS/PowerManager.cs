namespace XPlat.Device.Power
{
    using System;
    using System.Threading;

    using global::Foundation;

    using UIKit;

    using XPlat.UI.Core;

    public sealed partial class PowerManager : IPowerManager, IDisposable
    {
        private static readonly Lazy<PowerManager> CurrentPowerManager = new Lazy<PowerManager>(
            () => new PowerManager(),
            LazyThreadSafetyMode.PublicationOnly);

        private bool isDisposed;

        private NSObject level;

        private NSObject state;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerManager"/> class.
        /// </summary>
        public PowerManager()
        {
            UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;

            this.level = UIDevice.Notifications.ObserveBatteryLevelDidChange(this.OnBatteryNotification);
            this.state = UIDevice.Notifications.ObserveBatteryStateDidChange(this.OnBatteryNotification);
        }

        ~PowerManager()
        {
            this.Dispose(false);
        }

        /// <inheritdoc />
        public event EventHandler<BatteryStatus> BatteryStatusChanged;

        /// <inheritdoc />
        public event EventHandler<int> RemainingChargePercentChanged;

        public static PowerManager Current => CurrentPowerManager.Value;

        public BatteryStatus BatteryStatus => GetBatteryStatus();

        public int RemainingChargePercent => GetRemainingChargePercent();

        private void OnBatteryNotification(object sender, NSNotificationEventArgs args)
        {
            CoreDispatcher.Current.Run(
                () =>
                    {
                        this.BatteryStatusChanged?.Invoke(this, this.BatteryStatus);
                        this.RemainingChargePercentChanged?.Invoke(this, this.RemainingChargePercent);
                    });
        }

        private static BatteryStatus GetBatteryStatus()
        {
            switch (UIDevice.CurrentDevice.BatteryState)
            {
                case UIDeviceBatteryState.Charging:
                    return BatteryStatus.Charging;
                case UIDeviceBatteryState.Full:
                    return BatteryStatus.Idle;
                case UIDeviceBatteryState.Unplugged:
                    return BatteryStatus.Discharging;
            }

            return BatteryStatus.NotPresent;
        }

        private static int GetRemainingChargePercent()
        {
            return (int)(UIDevice.CurrentDevice.BatteryLevel * 100);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                UIDevice.CurrentDevice.BatteryMonitoringEnabled = false;
                if (this.level != null)
                {
                    this.level.Dispose();
                    this.level = null;
                }

                if (this.state != null)
                {
                    this.state.Dispose();
                    this.state = null;
                }
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}