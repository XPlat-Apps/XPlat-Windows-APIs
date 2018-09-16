namespace XPlat.Media.Capture.Extensions
{
    using System;

    public static class CameraCaptureUIMaxVideoResolutionExtensions
    {
#if WINDOWS_UWP
        public static CameraCaptureUIMaxVideoResolution ToInternalCameraCaptureUIMaxVideoResolution(
            this Windows.Media.Capture.CameraCaptureUIMaxVideoResolution resolution)
        {
            switch (resolution)
            {
                case Windows.Media.Capture.CameraCaptureUIMaxVideoResolution.HighestAvailable:
                    return CameraCaptureUIMaxVideoResolution.HighestAvailable;
                case Windows.Media.Capture.CameraCaptureUIMaxVideoResolution.LowDefinition:
                    return CameraCaptureUIMaxVideoResolution.LowDefinition;
                case Windows.Media.Capture.CameraCaptureUIMaxVideoResolution.StandardDefinition:
                    return CameraCaptureUIMaxVideoResolution.StandardDefinition;
                case Windows.Media.Capture.CameraCaptureUIMaxVideoResolution.HighDefinition:
                    return CameraCaptureUIMaxVideoResolution.HighDefinition;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resolution), resolution, null);
            }
        }

        public static Windows.Media.Capture.CameraCaptureUIMaxVideoResolution
            ToWindowsCameraCaptureUIMaxVideoResolution(this CameraCaptureUIMaxVideoResolution resolution)
        {
            switch (resolution)
            {
                case CameraCaptureUIMaxVideoResolution.HighestAvailable:
                    return Windows.Media.Capture.CameraCaptureUIMaxVideoResolution.HighestAvailable;
                case CameraCaptureUIMaxVideoResolution.LowDefinition:
                    return Windows.Media.Capture.CameraCaptureUIMaxVideoResolution.LowDefinition;
                case CameraCaptureUIMaxVideoResolution.StandardDefinition:
                    return Windows.Media.Capture.CameraCaptureUIMaxVideoResolution.StandardDefinition;
                case CameraCaptureUIMaxVideoResolution.HighDefinition:
                    return Windows.Media.Capture.CameraCaptureUIMaxVideoResolution.HighDefinition;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resolution), resolution, null);
            }
        }
#endif
    }
}