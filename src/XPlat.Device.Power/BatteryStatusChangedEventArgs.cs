namespace XPlat.Device.Power
{
    using System;

    public class BatteryStatusChangedEventArgs : EventArgs
    {
        public BatteryStatusChangedEventArgs(BatteryStatus batteryStatus, int remainingChargePercent)
        {
            this.BatteryStatus = batteryStatus;
            this.RemainingChargePercent = remainingChargePercent;
            this.CheckTime = DateTime.UtcNow;
        }

        public BatteryStatus BatteryStatus { get; }

        public int RemainingChargePercent { get; }

        public DateTime CheckTime { get; }
    }
}