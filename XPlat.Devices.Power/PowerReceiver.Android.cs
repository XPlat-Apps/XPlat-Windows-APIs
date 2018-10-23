#if __ANDROID__
namespace XPlat.Device.Power
{
    using System;

    using Android.Content;
    using Android.OS;

    public sealed class PowerReceiver : BroadcastReceiver
    {
        private readonly object obj = new object();

        private BatteryStatus previousStatus = BatteryStatus.NotPresent;

        private int previousRemainingCharge = 0;

        public event BatteryStatusChangedEventHandler Updated;

        /// <inheritdoc />
        public override void OnReceive(Context context, Intent intent)
        {
            if (this.Updated == null)
            {
                return;
            }

            int batteryLevel = intent.GetIntExtra(BatteryManager.ExtraLevel, -1);
            int batteryScale = intent.GetIntExtra(BatteryManager.ExtraScale, -1);
            int batteryStatus = intent.GetIntExtra(BatteryManager.ExtraStatus, -1);

            BatteryStatus status = BatteryStatus.NotPresent;

            switch (batteryStatus)
            {
                case (int)Android.OS.BatteryStatus.Charging:
                    status = BatteryStatus.Charging;
                    break;
                case (int)Android.OS.BatteryStatus.Discharging:
                    status = BatteryStatus.Discharging;
                    break;
                case (int)Android.OS.BatteryStatus.Full:
                case (int)Android.OS.BatteryStatus.NotCharging:
                    status = BatteryStatus.Idle;
                    break;
                case (int)Android.OS.BatteryStatus.Unknown:
                    status = BatteryStatus.NotPresent;
                    break;
            }

            int remainingChargePercent = (int)Math.Floor(batteryLevel * 100D / batteryScale);

            if (status != this.previousStatus || remainingChargePercent != this.previousRemainingCharge)
            {
                lock (this.obj)
                {
                    BatteryStatusChangedEventArgs eventArgs = new BatteryStatusChangedEventArgs(status, remainingChargePercent);

                    try
                    {
                        this.Updated?.Invoke(this, eventArgs);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        this.previousStatus = status;
                        this.previousRemainingCharge = remainingChargePercent;
                    }
                }
            }
        }
    }
}
#endif