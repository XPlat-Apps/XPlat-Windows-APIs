#if WINDOWS_UWP
namespace XPlat.Device.Power
{
    using System;
    using System.Threading;
    using XPlat.Device.Power.Extensions;

    /// <summary>Provides access to information about a device's battery and power supply status.</summary>
    public sealed class PowerManager : IPowerManager, IDisposable
    {
        private static readonly Lazy<PowerManager> CurrentPowerManager = new Lazy<PowerManager>(
            () => new PowerManager(),
            LazyThreadSafetyMode.PublicationOnly);

        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerManager"/> class.
        /// </summary>
        public PowerManager()
        {
            Windows.System.Power.PowerManager.BatteryStatusChanged += this.OnBatteryStatusChanged;
            Windows.System.Power.PowerManager.RemainingChargePercentChanged += this.OnRemainingChargePercentChanged;
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
            return Windows.System.Power.PowerManager.BatteryStatus.ToInternalBatteryStatus();
        }

        private static int GetRemainingChargePercent()
        {
            return Windows.System.Power.PowerManager.RemainingChargePercent;
        }

        private void OnRemainingChargePercentChanged(object sender, object e)
        {
            this.RemainingChargePercentChanged?.Invoke(this, this.RemainingChargePercent);
        }

        private void OnBatteryStatusChanged(object sender, object o)
        {
            this.BatteryStatusChanged?.Invoke(this, this.BatteryStatus);
        }

        private void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                }

                this.isDisposed = true;
            }
        }
    }
}
#endif