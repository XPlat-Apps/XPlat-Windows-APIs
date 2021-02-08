// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XPlat.ApplicationModel.DataTransfer
{
    /// <summary>Contains the data that a user wants to exchange with another app.</summary>
    public interface IDataPackage
    {
        /// <summary>Sets the text that a DataPackage contains.</summary>
        /// <param name="value">The text.</param>
        void SetText(string value);
    }
}