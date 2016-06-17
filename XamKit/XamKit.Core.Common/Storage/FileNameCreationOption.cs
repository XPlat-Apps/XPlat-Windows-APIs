namespace XamKit.Core.Common.Storage
{
    public enum FileNameCreationOption
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
        /// Creates the file with a unique identifier.
        /// </summary>
        GenerateUniqueIdentifier
    }
}