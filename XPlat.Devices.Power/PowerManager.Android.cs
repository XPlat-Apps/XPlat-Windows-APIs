#if __ANDROID__
namespace XPlat.Devices.Power
{
    using System;
    using System.Threading;

    using Android.App;
    using Android.Content;
    using Android.OS;

    using XPlat.Exceptions;

    /// <summary>Provides access to information about a device's battery and power supply status.</summary>
    public sealed class PowerManager : IPowerManager, IDisposable
    {
        private static readonly Lazy<PowerManager> CurrentPowerManager = new Lazy<PowerManager>(
            () => new PowerManager(),
            LazyThreadSafetyMode.PublicationOnly);

        private bool isDisposed;

        private PowerReceiver powerReceiver;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerManager"/> class.
        /// </summary>
        public PowerManager()
        {
            try
            {
                this.powerReceiver = new PowerReceiver();
                this.powerReceiver.Updated += this.OnPowerReceiverUpdated;
                Application.Context.RegisterReceiver(this.powerReceiver, new IntentFilter(Intent.ActionBatteryChanged));
            }
            catch (Exception ex)
            {
                throw new AppPermissionInvalidException("android.permission.BATTERY_STATS", ex.ToString(), ex);
            }
        }

        ~PowerManager()
        {
            this.Dispose(false);
        }

        /// <summary>Occurs when BatteryStatus changes.</summary>
        public event EventHandler<BatteryStatus> BatteryStatusChanged;

        /// <summary>Occurs when RemainingChargePercent changes.</summary>
        public event EventHandler<int> RemainingChargePercentChanged;

        /// <summary>
        /// Gets the current instance of the <see cref="PowerManager"/>.
        /// </summary>
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
            try
            {
                using (IntentFilter filter = new IntentFilter(Intent.ActionBatteryChanged))
                {
                    using (Intent battery = Application.Context.RegisterReceiver(null, filter))
                    {
                        int batteryStatus = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);

                        switch (batteryStatus)
                        {
                            case (int)Android.OS.BatteryStatus.Charging:
                                return BatteryStatus.Charging;
                            case (int)Android.OS.BatteryStatus.Discharging:
                                return BatteryStatus.Discharging;
                            case (int)Android.OS.BatteryStatus.Full:
                            case (int)Android.OS.BatteryStatus.NotCharging:
                                return BatteryStatus.Idle;
                        }
                    }
                }

                return BatteryStatus.NotPresent;
            }
            catch (Exception ex)
            {
                throw new AppPermissionInvalidException("android.permission.BATTERY_STATS", ex.ToString(), ex);
            }
        }

        private static int GetRemainingChargePercent()
        {
            try
            {
                using (IntentFilter filter = new IntentFilter(Intent.ActionBatteryChanged))
                {
                    using (Intent battery = Application.Context.RegisterReceiver(null, filter))
                    {
                        int level = battery.GetIntExtra(BatteryManager.ExtraLevel, -1);
                        int scale = battery.GetIntExtra(BatteryManager.ExtraScale, -1);

                        return (int)Math.Floor(level * 100D / scale);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AppPermissionInvalidException("android.permission.BATTERY_STATS", ex.ToString(), ex);
            }
        }

        private void OnPowerReceiverUpdated(object sender, BatteryStatusChangedEventArgs args)
        {
            if (args != null)
            {
                this.BatteryStatusChanged?.Invoke(this, args.BatteryStatus);
                this.RemainingChargePercentChanged?.Invoke(this, args.RemainingChargePercent);
            }
        }

        private void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                    // ToDo
                }

                this.isDisposed = true;
            }
        }
    }
}
#endif