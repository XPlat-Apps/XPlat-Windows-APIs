namespace XPlat.API.Device.Power
{
    public partial class PowerManager
    {
        private void Construct()
        {
            Windows.System.Power.PowerManager.BatteryStatusChanged += this.OnBatteryStatusChanged;
            Windows.System.Power.PowerManager.RemainingChargePercentChanged += this.OnRemainingChargePercentChanged;
        }

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
    }
}