// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileStoreProperty.cs" company="James Croft">
//   Copyright (c) James Croft.
// </copyright>
// <summary>
//   Defines a model for extended properties for files and folders.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XamKit.Core.Common.Storage
{
    using System;

    /// <summary>
    /// Defines a model for extended properties for files and folders.
    /// </summary>
    public class FileStoreProperty
    {
        public FileStoreProperty(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (value == null) throw new ArgumentNullException(nameof(value));

            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        public object Value { get; }
    }
}