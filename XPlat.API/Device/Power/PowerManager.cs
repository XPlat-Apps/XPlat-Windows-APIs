namespace XPlat.API.Device.Power
{
    using System;
    using System.Threading;

    public sealed partial class PowerManager : IPowerManager, IDisposable
    {
        private static readonly Lazy<PowerManager> CurrentPowerManager = new Lazy<PowerManager>(
            () => new PowerManager(),
            LazyThreadSafetyMode.PublicationOnly);

        private bool isDisposed;

#if !PORTABLE
        public PowerManager()
        {
            this.Construct();
        }

        /// <inheritdoc />
        ~PowerManager()
        {
            this.Dispose(false);
        }
#endif

        /// <inheritdoc />
        public event EventHandler<BatteryStatus> BatteryStatusChanged;

        /// <inheritdoc />
        public event EventHandler<int> RemainingChargePercentChanged;

        /// <summary>
        /// Gets the current instance of the <see cref="PowerManager"/>.
        /// </summary>
        public static PowerManager Current
        {
            get
            {
                return CurrentPowerManager.Value;
            }
        }

        public BatteryStatus BatteryStatus
        {
            get
            {
#if PORTABLE
                return BatteryStatus.NotPresent;
#else
                return GetBatteryStatus();
#endif
            }
        }

        public int RemainingChargePercent
        {
            get
            {
#if PORTABLE
                return 0;
#else
                return GetRemainingChargePercent();
#endif
            }
        }

        public void Dispose()
        {
#if !PORTABLE
            this.Dispose(true);
            GC.SuppressFinalize(this);
#endif
        }
    }
}