#if WINDOWS_UWP
namespace XPlat.ApplicationModel.DataTransfer
{
    /// <summary>Contains the data that a user wants to exchange with another app.</summary>
    public class DataPackage : IDataPackage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPackage"/> class.
        /// </summary>
        public DataPackage()
        {
            this.Originator = new Windows.ApplicationModel.DataTransfer.DataPackage();
        }

        public static implicit operator Windows.ApplicationModel.DataTransfer.DataPackage(DataPackage dataPackage)
        {
            return dataPackage?.Originator;
        }

        /// <summary>Gets the originating Windows DataPackage instance.</summary>
        public Windows.ApplicationModel.DataTransfer.DataPackage Originator { get; }

        /// <summary>Sets the text that a DataPackage contains.</summary>
        /// <param name="value">The text.</param>
        public void SetText(string value)
        {
            this.Originator.SetText(value);
        }
    }
}
#endif