namespace XPlat.Media.Capture
{
    using System.Threading.Tasks;

    using XPlat.Storage;

    public interface ICameraCaptureUI
    {
        Task<IStorageFile> CaptureFileAsync(CameraCaptureUIMode mode);
    }
}
