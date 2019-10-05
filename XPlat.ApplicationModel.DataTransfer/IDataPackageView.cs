namespace XPlat.ApplicationModel.DataTransfer
{
    using System.Threading.Tasks;

    /// <summary>A read-only version of a DataPackage. Apps that receive shared content get this object when acquiring content.</summary>
    public interface IDataPackageView
    {
        /// <summary>Gets the text in the DataPackageView object.</summary>
        /// <returns>The text.</returns>
        Task<string> GetTextAsync();
    }
}