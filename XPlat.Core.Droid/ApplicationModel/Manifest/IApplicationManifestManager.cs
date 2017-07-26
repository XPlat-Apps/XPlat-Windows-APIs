namespace XPlat.Droid.ApplicationModel.Manifest
{
    using System.Threading.Tasks;

    using Android.App;

    public interface IApplicationManifestManager
    {
        /// <summary>
        /// Gets or sets the current activity.
        /// </summary>
        Activity CurrentActivity { get; set; }

        /// <summary>
        /// Gets the current request id.
        /// </summary>
        int CurrentRequestId { get; }

        /// <summary>
        /// Checks whether a content provider exists.
        /// </summary>
        /// <param name="providerName">
        /// The name of the provider to check.
        /// </param>
        /// <returns>
        /// When this method completes, true will be returned if the provider exists.
        /// </returns>
        bool CheckContentProviderExists(string providerName);

        /// <summary>
        /// Requests for permissions to be granted.
        /// </summary>
        /// <param name="permissions">
        /// The list of permissions that need to be granted.
        /// </param>
        /// <returns>
        /// When this method completes, true will be returned if all the permissions requested were granted.
        /// </returns>
        Task<bool> RequestPermissionAsync(params string[] permissions);

        /// <summary>
        /// Checks whether a permission is granted.
        /// </summary>
        /// <param name="permission">
        /// The permission to check.
        /// </param>
        /// <returns>
        /// When this method completes, true will be returned if the permission has been granted.
        /// </returns>
        bool CheckPermissionGranted(string permission);
    }
}