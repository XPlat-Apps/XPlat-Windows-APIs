using System;

namespace XPlat.UI.Core.Extensions
{
    public static class CoreDispatcherPriorityExtensions
    {
#if WINDOWS_UWP
        public static CoreDispatcherPriority ToInternalCoreDispatcherPriority(
            this Windows.UI.Core.CoreDispatcherPriority priority)
        {
            switch (priority)
            {
                case Windows.UI.Core.CoreDispatcherPriority.Idle:
                case Windows.UI.Core.CoreDispatcherPriority.Low:
                    return CoreDispatcherPriority.Low;
                case Windows.UI.Core.CoreDispatcherPriority.Normal:
                    return CoreDispatcherPriority.Normal;
                case Windows.UI.Core.CoreDispatcherPriority.High:
                    return CoreDispatcherPriority.High;
                default:
                    throw new ArgumentOutOfRangeException(nameof(priority), priority, null);
            }
        }

        public static Windows.UI.Core.CoreDispatcherPriority ToWindowsCoreDispatcherPriority(
            this CoreDispatcherPriority priority)
        {
            switch (priority)
            {
                case CoreDispatcherPriority.Low:
                    return Windows.UI.Core.CoreDispatcherPriority.Low;
                case CoreDispatcherPriority.Normal:
                    return Windows.UI.Core.CoreDispatcherPriority.Normal;
                case CoreDispatcherPriority.High:
                    return Windows.UI.Core.CoreDispatcherPriority.High;
                default:
                    throw new ArgumentOutOfRangeException(nameof(priority), priority, null);
            }
        }
#endif
    }
}