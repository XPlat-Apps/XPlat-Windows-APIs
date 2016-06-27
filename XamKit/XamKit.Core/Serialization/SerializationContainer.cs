// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationContainer.cs" company="James Croft">
//   Copyright (c) James Croft.
// </copyright>
// <summary>
//   Defines a model for containing serialized data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XamKit.Core.Serialization
{
    /// <summary>
    /// Defines a model for containing serialized data.
    /// </summary>
    internal sealed class SerializationContainer
    {
        /// <summary>
        /// Gets or sets the type of the serialized data.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the serialized data.
        /// </summary>
        public string Data { get; set; }
    }
}