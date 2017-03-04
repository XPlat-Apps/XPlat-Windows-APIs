namespace XPlat.API.Device.Display
{
    public interface IDisplayRequest
    {
        void RequestActive();

        void RequestRelease();
    }
}