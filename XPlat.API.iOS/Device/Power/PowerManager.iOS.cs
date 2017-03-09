namespace XPlat.API.Device.Power
{
    using global::Foundation;

    using UIKit;

    using XPlat.API.UI.Core;

    public partial class PowerManager
    {
        private NSObject level;

        private NSObject state;

        private void Construct()
        {
            UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;

            this.level = UIDevice.Notifications.ObserveBatteryLevelDidChange(this.OnBatteryNotification);
            this.state = UIDevice.Notifications.ObserveBatteryStateDidChange(this.OnBatteryNotification);
        }

        private void OnBatteryNotification(object sender, NSNotificationEventArgs args)
        {
            MainDispatcher.Run(
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
    }
}