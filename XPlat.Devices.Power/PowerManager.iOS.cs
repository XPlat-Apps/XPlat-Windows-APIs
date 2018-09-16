#if __IOS__
namespace XPlat.Devices.Power
{
    using System;
    using System.Threading;

    using Foundation;

    using UIKit;

    /// <summary>Provides access to information about a device's battery and power supply status.</summary>
    public sealed class PowerManager : IPowerManager, IDisposable
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

        /// <summary>Occurs when BatteryStatus changes.</summary>
        public event EventHandler<BatteryStatus> BatteryStatusChanged;

        /// <summary>Occurs when RemainingChargePercent changes.</summary>
        public event EventHandler<int> RemainingChargePercentChanged;

        public static PowerManager Current => CurrentPowerManager.Value;

        /// <summary>Gets the device's battery status.</summary>
        public BatteryStatus BatteryStatus => GetBatteryStatus();

        /// <summary>Gets the total percentage of charge remaining from all batteries connected to the device.</summary>
        public int RemainingChargePercent => GetRemainingChargePercent();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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
                case UIDeviceBatteryState.Unknown:
                    return BatteryStatus.NotPresent;
                default:
                    throw new ArgumentOutOfRangeException(nameof(UIDevice.CurrentDevice.BatteryState), UIDevice.CurrentDevice.BatteryState, null);
            }
        }

        private static int GetRemainingChargePercent()
        {
            return (int)(UIDevice.CurrentDevice.BatteryLevel * 100);
        }

        private void OnBatteryNotification(object sender, NSNotificationEventArgs args)
        {
            this.BatteryStatusChanged?.Invoke(this, this.BatteryStatus);
            this.RemainingChargePercentChanged?.Invoke(this, this.RemainingChargePercent);
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
    }
}
#endif