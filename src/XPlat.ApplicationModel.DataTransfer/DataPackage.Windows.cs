// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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

        /// <summary>Gets the originating Windows DataPackage instance.</summary>
        public Windows.ApplicationModel.DataTransfer.DataPackage Originator { get; }

        /// <summary>
        /// Allows conversion of a <see cref="Windows.ApplicationModel.DataTransfer.DataPackage"/> to the <see cref="DataPackage"/> without direct casting.
        /// </summary>
        /// <param name="dataPackage">
        /// The <see cref="Windows.ApplicationModel.DataTransfer.DataPackage"/>.
        /// </param>
        /// <returns>
        /// The <see cref="DataPackage"/>.
        /// </returns>
        public static implicit operator Windows.ApplicationModel.DataTransfer.DataPackage(DataPackage dataPackage)
        {
            return dataPackage?.Originator;
        }

        /// <summary>Sets the text that a DataPackage contains.</summary>
        /// <param name="value">The text.</param>
        public void SetText(string value)
        {
            this.Originator.SetText(value);
        }
    }
}
#endif