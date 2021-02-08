// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XPlat.ApplicationModel
{
    using System;

    /// <summary>Provides information about a package.</summary>
    public interface IPackage3
    {
        /// <summary>Gets the date on which the application package was installed or last updated.</summary>
        DateTimeOffset InstalledDate { get; }
    }
}
