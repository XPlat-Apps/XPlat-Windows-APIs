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
        internal DataPackageView(Windows.ApplicationModel.DataTransfer.DataPackageView originator)
        {
            this.Originator = originator;
        }

        public static implicit operator DataPackageView(Windows.ApplicationModel.DataTransfer.DataPackageView dataPackageView)
        {
            return new DataPackageView(dataPackageView);
        }

        /// <summary>Gets the originating Windows DataPackageView instance.</summary>
        public Windows.ApplicationModel.DataTransfer.DataPackageView Originator { get; }

        /// <summary>Gets the text in the DataPackageView object.</summary>
        /// <returns>The text.</returns>
        public async Task<string> GetTextAsync()
        {
            return await this.Originator.GetTextAsync();
        }
    }
}
#endif