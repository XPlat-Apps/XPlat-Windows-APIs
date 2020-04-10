namespace XPlat.Media.Capture
{
    /// <summary>Determines whether the user interface for capturing from the attached camera allows capture of photos, videos, or both photos and videos.</summary>
    public enum CameraCaptureUIMode
    {
        /// <summary>Either a photo or video can be captured.</summary>
        PhotoOrVideo,

        /// <summary>The user can only capture a photo.</summary>
        Photo,

        /// <summary>The user can only capture a video.</summary>
        Video,
    }
}