namespace XPlat.Device.Power
{
    public static partial class Extensions
    {
        public static BatteryStatus ToBatteryStatus(this Windows.System.Power.BatteryStatus status)
        {
            switch (status)
            {
                case Windows.System.Power.BatteryStatus.Discharging:
                    return BatteryStatus.Discharging;
                case Windows.System.Power.BatteryStatus.Idle:
                    return BatteryStatus.Idle;
                case Windows.System.Power.BatteryStatus.Charging:
                    return BatteryStatus.Charging;
            }

            return BatteryStatus.NotPresent;
        }
    }
}
