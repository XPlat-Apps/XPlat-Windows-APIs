#if __ANDROID__
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
        }

        internal string Text { get; private set; }

        /// <summary>Sets the text that a DataPackage contains.</summary>
        /// <param name="value">The text.</param>
        public void SetText(string value)
        {
            this.Text = value;
        }
    }
}
#endif