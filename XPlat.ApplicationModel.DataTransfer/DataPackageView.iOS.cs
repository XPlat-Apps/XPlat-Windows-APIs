#if __IOS__
namespace XPlat.ApplicationModel.DataTransfer
{
    using System.Threading.Tasks;

    /// <summary>A read-only version of a DataPackage. Apps that receive shared content get this object when acquiring content.</summary>
    public class DataPackageView : DataPackage, IDataPackageView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPackageView"/> class.
        /// </summary>
        internal DataPackageView()
        {
        }

        /// <summary>Gets the text in the DataPackageView object.</summary>
        /// <returns>The text.</returns>
        public Task<string> GetTextAsync()
        {
            return Task.FromResult(this.Text);
        }
    }
}
#endif