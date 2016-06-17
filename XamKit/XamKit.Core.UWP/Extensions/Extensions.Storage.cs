namespace XamKit.Core.Extensions
{
    using System;
    using System.IO;

    using Windows.Storage;

    using XamKit.Core.Common.Storage;

    public static partial class Extensions
    {
        /// <summary>
        /// Converts the common <see cref="FileStoreCreationOption"/> to <see cref="CreationCollisionOption"/>.
        /// </summary>
        /// <param name="option">
        /// The specified option.
        /// </param>
        /// <returns>
        /// Returns the Windows.Storage <see cref="CreationCollisionOption"/> for the specified option.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the option is not a supported value.
        /// </exception>
        public static CreationCollisionOption ToCreationCollisionOption(this FileStoreCreationOption option)
        {
            switch (option)
            {
                case FileStoreCreationOption.ThrowExceptionIfExists:
                    return CreationCollisionOption.FailIfExists;
                case FileStoreCreationOption.ReplaceIfExists:
                    return CreationCollisionOption.ReplaceExisting;
                case FileStoreCreationOption.GenerateUniqueIdentifier:
                    return CreationCollisionOption.GenerateUniqueName;
                case FileStoreCreationOption.OpenIfExists:
                    return CreationCollisionOption.OpenIfExists;
            }

            throw new ArgumentException("The specified option is not a supported value.");
        }

        /// <summary>
        /// Converts the common <see cref="FileAccessMode"/> to <see cref="FileAccess"/>.
        /// </summary>
        /// <param name="option">
        /// The specified option.
        /// </param>
        /// <returns>
        /// Returns the Windows.Storage <see cref="FileAccessMode"/> for the specified option.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the option is not a supported value.
        /// </exception>
        public static FileAccessMode ToFileAccessMode(this FileAccessOption option)
        {
            switch (option)
            {
                case FileAccessOption.ReadOnly:
                    return FileAccessMode.Read;
                case FileAccessOption.ReadAndWrite:
                    return FileAccessMode.ReadWrite;
            }

            throw new ArgumentException("The specified option is not a supported value.");
        }

        /// <summary>
        /// Converts the common <see cref="FileNameCreationOption"/> to <see cref="NameCollisionOption"/>.
        /// </summary>
        /// <param name="option">
        /// The specified option.
        /// </param>
        /// <returns>
        /// Returns the Windows.Storage <see cref="NameCollisionOption"/> for the specified option.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the option is not a supported value.
        /// </exception>
        public static NameCollisionOption ToNameCollisionOption(this FileNameCreationOption option)
        {
            switch (option)
            {
                case FileNameCreationOption.ThrowExceptionIfExists:
                    return NameCollisionOption.FailIfExists;
                case FileNameCreationOption.ReplaceIfExists:
                    return NameCollisionOption.ReplaceExisting;
                case FileNameCreationOption.GenerateUniqueIdentifier:
                    return NameCollisionOption.GenerateUniqueName;
            }

            throw new ArgumentException("The specified option is not a supported value.");
        }
    }
}