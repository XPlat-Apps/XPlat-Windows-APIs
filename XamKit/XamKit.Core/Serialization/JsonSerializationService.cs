// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonSerializationService.cs" company="James Croft">
//   Copyright (c) James Croft.
// </copyright>
// <summary>
//   Defines the data serialization service for JSON.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XamKit.Core.Serialization
{
    using System;
    using System.Runtime.Serialization.Formatters;

    using Newtonsoft.Json;

    using XamKit.Core.Common.Serialization;

    /// <summary>
    /// Defines the data serialization service for JSON.
    /// </summary>
    public class JsonSerializationService : ISerializationService
    {
        internal JsonSerializationService()
        {
            this.Settings = new JsonSerializerSettings
                                {
                                    Formatting = Formatting.None,
                                    TypeNameHandling = TypeNameHandling.Auto,
                                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                };
        }

        /// <summary>
        /// Gets the JSON serializer's settings.
        /// </summary>
        public JsonSerializerSettings Settings { get; }

        /// <summary>
        /// Serializes the specified object to a string.
        /// </summary>
        /// <param name="obj">
        /// The object to serialize.
        /// </param>
        /// <returns>
        /// Returns the serialized string representation of the specified object.
        /// </returns>
        public string Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(obj.ToString()))
            {
                return string.Empty;
            }

            var container = new SerializationContainer
                                {
                                    Type = obj.GetType().AssemblyQualifiedName,
                                    Data =
                                        JsonConvert.SerializeObject(
                                            obj,
                                            Formatting.None,
                                            this.Settings)
                                };

            return JsonConvert.SerializeObject(container);
        }

        /// <summary>
        /// Deserializes the specified string to an object.
        /// </summary>
        /// <param name="strObj">
        /// The stringified object.
        /// </param>
        /// <returns>
        /// Returns the deserialized object.
        /// </returns>
        public object Deserialize(string strObj)
        {
            if (string.IsNullOrWhiteSpace(strObj))
            {
                return null;
            }

            var container = JsonConvert.DeserializeObject<SerializationContainer>(strObj);
            var type = Type.GetType(container.Type);
            return JsonConvert.DeserializeObject(container.Data, type, this.Settings);
        }

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
        public T Deserialize<T>(string strObj)
        {
            var result = this.Deserialize(strObj);
            if (result != null)
            {
                return (T)result;
            }

            return default(T);
        }
    }
}