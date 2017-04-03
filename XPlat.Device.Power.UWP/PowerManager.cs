namespace XPlat.Device.Power
{
    using System.Threading;
    using System;

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

        /// <inheritdoc />
        ~PowerManager()
        {
            this.Dispose(false);
        }

        /// <inheritdoc />
        public event EventHandler<BatteryStatus> BatteryStatusChanged;

        /// <inheritdoc />
        public event EventHandler<int> RemainingChargePercentChanged;

        /// <summary>
        /// Gets the current instance of the <see cref="PowerManager"/>.
        /// </summary>
        public static PowerManager Current => CurrentPowerManager.Value;

        /// <inheritdoc />
        public BatteryStatus BatteryStatus => GetBatteryStatus();

        /// <inheritdoc />
        public int RemainingChargePercent => GetRemainingChargePercent();

        private void OnRemainingChargePercentChanged(object sender, object e)
        {
            this.RemainingChargePercentChanged?.Invoke(this, this.RemainingChargePercent);
        }

        private void OnBatteryStatusChanged(object sender, object o)
        {
            this.BatteryStatusChanged?.Invoke(this, this.BatteryStatus);
        }

        private static BatteryStatus GetBatteryStatus()
        {
            return Windows.System.Power.PowerManager.BatteryStatus.ToBatteryStatus();
        }

        private static int GetRemainingChargePercent()
        {
            return Windows.System.Power.PowerManager.RemainingChargePercent;
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

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
