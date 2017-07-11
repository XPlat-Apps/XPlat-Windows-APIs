namespace XPlat.Media.Capture
{
    using System.Threading.Tasks;

    using XPlat.Storage;

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
    }
}