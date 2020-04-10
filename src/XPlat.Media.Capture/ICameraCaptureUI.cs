namespace XPlat.Media.Capture
{
    using System.Threading.Tasks;

    using XPlat.Storage;

    /// <summary>Provides a full window UI for capturing video and photos from a camera.</summary>
    public interface ICameraCaptureUI
    {
        /// <summary>Provides settings for capturing photos.</summary>
        CameraCaptureUIPhotoCaptureSettings PhotoSettings { get; }

        /// <summary>Provides settings for capturing videos. The settings include maximum resolution, maximum duration, and whether or not to allow trimming.</summary>
        CameraCaptureUIVideoCaptureSettings VideoSettings { get; }

        /// <summary>Launches the CameraCaptureUI user interface.</summary>
        /// <returns>When this operation completes, an IStorageFile object is returned.</returns>
        /// <param name="mode">Specifies whether the user interface that will be shown allows the user to capture a photo, capture a video, or capture both photos and videos.</param>
        Task<IStorageFile> CaptureFileAsync(CameraCaptureUIMode mode);
    }
}