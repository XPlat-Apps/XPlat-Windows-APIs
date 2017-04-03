namespace XPlat.Device.Power
{
    using System;

    public delegate void BatteryStatusChangedEventHandler(object sender, BatteryStatusChangedEventArgs args);

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

        /// <summary>
        /// Gets the date/time that this event argument was created.
        /// </summary>
        public DateTime CheckTime { get; }
    }
}