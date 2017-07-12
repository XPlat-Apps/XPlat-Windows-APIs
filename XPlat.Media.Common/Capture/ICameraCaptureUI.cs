namespace XPlat.Media.Capture
{
    using System.Threading.Tasks;

    using XPlat.Storage;

    /// <summary>
    /// Provides a full window UI for capturing audio, video, and photos from a camera.
    /// </summary>
    public interface ICameraCaptureUI
    {
        /// <summary>
        /// Launches the camera capture user interface.
        /// </summary>
        /// <returns>
        /// When this operation completes, an IStorageFile object is returned.
        /// </returns>
        /// <param name="mode">
        /// Specifies whether the user interface that will be shown allows the user to capture a photo, capture a video, or capture both photos and videos.
        /// </param>
        Task<IStorageFile> CaptureFileAsync(CameraCaptureUIMode mode);

        /// <summary>
        /// Provides settings for capturing photos.
        /// </summary>
        CameraCaptureUIPhotoCaptureSettings PhotoSettings { get; }

        /// <summary>
        /// Provides settings for capturing videos.
        /// </summary>
        CameraCaptureUIVideoCaptureSettings VideoSettings { get; }
    }
}