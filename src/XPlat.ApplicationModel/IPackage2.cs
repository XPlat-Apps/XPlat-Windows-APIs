// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XPlat.ApplicationModel
{
    using System;

    /// <summary>Provides information about a package.</summary>
    public interface IPackage2
    {
        /// <summary>Gets the display name of the package.</summary>
        string DisplayName { get; }

        /// <summary>Gets the logo of the package.</summary>
        Uri Logo { get; }

        /// <summary>Indicates whether the package is installed in development mode.</summary>
        bool IsDevelopmentMode { get; }
    }
}