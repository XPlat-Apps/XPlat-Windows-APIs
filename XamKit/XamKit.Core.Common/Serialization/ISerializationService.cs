// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISerializationService.cs" company="James Croft">
//   Copyright (c) James Croft.
// </copyright>
// <summary>
//   Defines the interface for serialization services.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XamKit.Core.Common.Serialization
{
    /// <summary>
    /// Defines the interface for serialization services.
    /// </summary>
    public interface ISerializationService
    {
        /// <summary>
        /// Serializes the specified object to a string.
        /// </summary>
        /// <param name="obj">
        /// The object to serialize.
        /// </param>
        /// <returns>
        /// Returns the serialized string representation of the specified object.
        /// </returns>
        string Serialize(object obj);

        /// <summary>
        /// Deserializes the specified string to an object.
        /// </summary>
        /// <param name="strObj">
        /// The stringified object.
        /// </param>
        /// <returns>
        /// Returns the deserialized object.
        /// </returns>
        object Deserialize(string strObj);

        /// <summary>
        /// Deserializes the specified string to specified object type.
        /// </summary>
        /// <param name="strObj">
        /// The stringified object.
        /// </param>
        /// <typeparam name="T">
        /// The type to deserialize to.
        /// </typeparam>
        /// <returns>
        /// Returns the deserialized object.
        /// </returns>
        T Deserialize<T>(string strObj);
    }
}