namespace XPlat.Device.Display
{
    public class DisplayRequest : IDisplayRequest
    {
        private readonly Windows.System.Display.DisplayRequest request;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayRequest"/> class.
        /// </summary>
        public DisplayRequest()
        {
            this.request = new Windows.System.Display.DisplayRequest();
        }

        /// <inheritdoc />
        public void RequestActive()
        {
            this.request?.RequestActive();
        }

        /// <inheritdoc />
        public void RequestRelease()
        {
            this.request?.RequestRelease();
        }
    }
}