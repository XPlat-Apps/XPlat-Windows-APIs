// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if WINDOWS_UWP
namespace XPlat.ApplicationModel.DataTransfer
{
    using System;
    using System.Threading.Tasks;

    /// <summary>A read-only version of a DataPackage. Apps that receive shared content get this object when acquiring content.</summary>
    public class DataPackageView : IDataPackageView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPackageView"/> class.
        /// </summary>
        /// <param name="originator">
        /// The originating Windows DataPackageView instance.
        /// </param>
        internal DataPackageView(Windows.ApplicationModel.DataTransfer.DataPackageView originator)
        {
            this.Originator = originator;
        }

        /// <summary>Gets the originating Windows DataPackageView instance.</summary>
        public Windows.ApplicationModel.DataTransfer.DataPackageView Originator { get; }

        /// <summary>
        /// Allows conversion of a <see cref="Windows.ApplicationModel.DataTransfer.DataPackageView"/> to the <see cref="DataPackageView"/> without direct casting.
        /// </summary>
        /// <param name="dataPackageView">
        /// The <see cref="Windows.ApplicationModel.DataTransfer.DataPackageView"/>.
        /// </param>
        /// <returns>
        /// The <see cref="DataPackageView"/>.
        /// </returns>
        public static implicit operator DataPackageView(Windows.ApplicationModel.DataTransfer.DataPackageView dataPackageView)
        {
            return new DataPackageView(dataPackageView);
        }
        
        /// <summary>Gets the text in the DataPackageView object.</summary>
        /// <returns>The text.</returns>
        public async Task<string> GetTextAsync()
        {
            return await this.Originator.GetTextAsync();
        }
    }
}
#endif