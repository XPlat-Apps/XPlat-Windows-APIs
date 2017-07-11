namespace XPlat.Media
{
    using Windows.Media.Capture;

    public static partial class Extensions
    {
        public static CameraCaptureUIMaxPhotoResolution ToCameraCaptureUIMaxPhotoResolution(
            this XPlat.Media.Capture.CameraCaptureUIMaxPhotoResolution resolution)
        {
            switch (resolution)
            {
                case Capture.CameraCaptureUIMaxPhotoResolution.VerySmallQvga:
                    return CameraCaptureUIMaxPhotoResolution.VerySmallQvga;
                case Capture.CameraCaptureUIMaxPhotoResolution.SmallVga:
                    return CameraCaptureUIMaxPhotoResolution.SmallVga;
                case Capture.CameraCaptureUIMaxPhotoResolution.MediumXga:
                    return CameraCaptureUIMaxPhotoResolution.MediumXga;
                case Capture.CameraCaptureUIMaxPhotoResolution.Large3M:
                    return CameraCaptureUIMaxPhotoResolution.Large3M;
                case Capture.CameraCaptureUIMaxPhotoResolution.VeryLarge5M:
                    return CameraCaptureUIMaxPhotoResolution.VeryLarge5M;
                default: return CameraCaptureUIMaxPhotoResolution.HighestAvailable;
            }
        }
    }
}