namespace XPlat.Media.Capture.Extensions
{
    using System;

    public static class CameraCaptureUIMaxPhotoResolutionExtensions
    {
#if WINDOWS_UWP
        public static CameraCaptureUIMaxPhotoResolution ToInternalCameraCaptureUIMaxPhotoResolution(
            this Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution resolution)
        {
            switch (resolution)
            {
                case Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.HighestAvailable:
                    return CameraCaptureUIMaxPhotoResolution.HighestAvailable;
                case Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.VerySmallQvga:
                    return CameraCaptureUIMaxPhotoResolution.VerySmallQvga;
                case Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.SmallVga:
                    return CameraCaptureUIMaxPhotoResolution.SmallVga;
                case Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.MediumXga:
                    return CameraCaptureUIMaxPhotoResolution.MediumXga;
                case Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.Large3M:
                    return CameraCaptureUIMaxPhotoResolution.Large3M;
                case Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.VeryLarge5M:
                    return CameraCaptureUIMaxPhotoResolution.VeryLarge5M;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resolution), resolution, null);
            }
        }

        public static Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution
            ToWindowsCameraCaptureUIMaxPhotoResolution(this CameraCaptureUIMaxPhotoResolution resolution)
        {
            switch (resolution)
            {
                case CameraCaptureUIMaxPhotoResolution.HighestAvailable:
                    return Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.HighestAvailable;
                case CameraCaptureUIMaxPhotoResolution.VerySmallQvga:
                    return Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.VerySmallQvga;
                case CameraCaptureUIMaxPhotoResolution.SmallVga:
                    return Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.SmallVga;
                case CameraCaptureUIMaxPhotoResolution.MediumXga:
                    return Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.MediumXga;
                case CameraCaptureUIMaxPhotoResolution.Large3M:
                    return Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.Large3M;
                case CameraCaptureUIMaxPhotoResolution.VeryLarge5M:
                    return Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution.VeryLarge5M;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resolution), resolution, null);
            }
        }
#endif
    }
}