namespace XPlat.Droid.ApplicationModel.Manifest
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content.PM;
    using Android.Support.V4.App;
    using Android.Support.V4.Content;

    public class ApplicationManifestManager : IApplicationManifestManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationManifestManager"/> class.
        /// </summary>
        /// <remarks>
        /// When using the default constructor, the CurrentActivity property must be set in order to request permissions.
        /// </remarks>
        public ApplicationManifestManager()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationManifestManager"/> class.
        /// </summary>
        /// <param name="activity">
        /// The current active Activity.
        /// </param>
        public ApplicationManifestManager(Activity activity)
        {
            this.CurrentActivity = activity;
        }

        /// <inheritdoc />
        public Activity CurrentActivity { get; set; }

        /// <inheritdoc />
        public int CurrentRequestId { get; private set; }

        /// <inheritdoc />
        public bool CheckContentProviderExists(string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException(nameof(providerName));
            }

            if (this.CurrentActivity == null)
            {
                throw new ArgumentException(
                    "The CurrentActivity property must be set in order to check for content providers.");
            }

            PackageInfo myPackage = this.CurrentActivity.PackageManager.GetInstalledPackages(PackageInfoFlags.Providers)
                .FirstOrDefault(
                    x => x.PackageName.Equals(
                        this.CurrentActivity.PackageName,
                        StringComparison.CurrentCultureIgnoreCase));

            return myPackage != null
                   && myPackage.Providers.Any(x => x.Name.Contains(providerName, CompareOptions.IgnoreCase));
        }

        /// <inheritdoc />
        public Task<bool> RequestPermissionAsync(params string[] permissions)
        {
            this.GenerateRequestId();

            TaskCompletionSource<bool> newTcs = new TaskCompletionSource<bool>(this.CurrentRequestId);

            if (permissions == null || !permissions.Any())
            {
                throw new ArgumentNullException(nameof(permissions));
            }

            if (this.CurrentActivity == null)
            {
                throw new ArgumentException(
                    "The CurrentActivity property must be set in order to request permissions.");
            }

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                List<string> permissionsToRequest = permissions.Where(permission => !this.CheckPermissionGranted(permission))
                    .ToList();

                if (permissionsToRequest.Count > 0)
                {
                    ActivityCompat.RequestPermissions(
                        this.CurrentActivity,
                        permissionsToRequest.ToArray(),
                        this.CurrentRequestId);
                    newTcs.SetResult(false);
                }
                else
                {
                    newTcs.SetResult(true);
                }
            }
            else
            {
                newTcs.SetResult(true);
            }

            return newTcs.Task;

        }

        /// <inheritdoc />
        public bool CheckPermissionGranted(string permission)
        {
            return ContextCompat.CheckSelfPermission(this.CurrentActivity, permission) == Permission.Granted;
        }

        private void GenerateRequestId()
        {
            if (this.CurrentRequestId == int.MaxValue)
            {
                this.CurrentRequestId = 0;
            }

            this.CurrentRequestId++;
        }
    }
}