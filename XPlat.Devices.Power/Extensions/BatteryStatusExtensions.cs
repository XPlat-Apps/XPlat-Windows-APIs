namespace XPlat.Device.Power.Extensions
{
    using System;

    public static class BatteryStatusExtensions
    {
#if WINDOWS_UWP
        public static BatteryStatus ToInternalBatteryStatus(this Windows.System.Power.BatteryStatus status)
        {
            switch (status)
            {
                case Windows.System.Power.BatteryStatus.NotPresent:
                    return BatteryStatus.NotPresent;
                case Windows.System.Power.BatteryStatus.Discharging:
                    return BatteryStatus.Discharging;
                case Windows.System.Power.BatteryStatus.Idle:
                    return BatteryStatus.Idle;
                case Windows.System.Power.BatteryStatus.Charging:
                    return BatteryStatus.Charging;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
#endif
    }
}