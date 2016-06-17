namespace XamKit.Core.Common.Storage
{
    /// <summary>
    /// Defines the file store creation options.
    /// </summary>
    public enum FileStoreCreationOption
    {
        /// <summary>
        /// Throws an exception if the file already exists.
        /// </summary>
        ThrowExceptionIfExists,

        /// <summary>
        /// Replaces the file if it already exists.
        /// </summary>
        ReplaceIfExists,

        /// <summary>
        /// Opens the file if it already exists.
        /// </summary>
        OpenIfExists,

        /// <summary>
        /// Creates the file with a unique identifier.
        /// </summary>
        GenerateUniqueIdentifier
    }
}