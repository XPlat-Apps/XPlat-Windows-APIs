// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationService.cs" company="James Croft">
//   Copyright (c) James Croft.
// </copyright>
// <summary>
//   Defines the service for serializing data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XamKit.Core.Serialization
{
    using System;
    using System.Threading;

    using XamKit.Core.Common.Serialization;

    /// <summary>
    /// Defines the service for serializing data.
    /// </summary>
    public class SerializationService
    {
        private readonly Lazy<ISerializationService> json =
            new Lazy<ISerializationService>(CreateJsonSerializationService, LazyThreadSafetyMode.PublicationOnly);

        public ISerializationService Json
        {
            get
            {
                var s = this.json.Value;
                if (s == null)
                {
                    throw new NotImplementedException(
                        "The library you're calling SerializationService from is not supported.");
                }
                return s;
            }
        }

        private static ISerializationService CreateJsonSerializationService()
        {
            return new JsonSerializationService();
        }
    }
}