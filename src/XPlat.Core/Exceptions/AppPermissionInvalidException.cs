// <copyright file="AppPermissionInvalidException.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Exceptions
{
    using System;

    /// <summary>
    /// Defines an exception that is thrown when an application permission is not set.
    /// </summary>
    public class AppPermissionInvalidException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppPermissionInvalidException"/> class.
        /// </summary>
        /// <param name="permission">
        /// The permission which could not be found.
        /// </param>
        /// <param name="message">
        /// The message to store.
        /// </param>
        public AppPermissionInvalidException(string permission, string message)
            : this(permission, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppPermissionInvalidException"/> class.
        /// </summary>
        /// <param name="permission">
        /// The permission which could not be found.
        /// </param>
        /// <param name="message">
        /// The message to store.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public AppPermissionInvalidException(string permission, string message, Exception innerException)
            : base(message, innerException)
        {
            this.PermissionCode = permission;
        }

        /// <summary>
        /// Gets the permission which could not be found.
        /// </summary>
        public string PermissionCode { get; }
    }
}