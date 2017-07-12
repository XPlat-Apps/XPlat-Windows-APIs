namespace XPlat.Droid.ApplicationModel.Manifest
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content.PM;
    using Android.Support.V4.App;
    using Android.Support.V4.Content;

    public class ApplicationPermissionManager : IApplicationPermissionManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationPermissionManager"/> class.
        /// </summary>
        /// <remarks>
        /// When using the default constructor, the CurrentActivity property must be set in order to request permissions.
        /// </remarks>
        public ApplicationPermissionManager()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationPermissionManager"/> class.
        /// </summary>
        /// <param name="activity">
        /// The current active Activity.
        /// </param>
        public ApplicationPermissionManager(Activity activity)
        {
            this.CurrentActivity = activity;
        }

        /// <inheritdoc />
        public Activity CurrentActivity { get; set; }

        /// <inheritdoc />
        public int CurrentRequestId { get; private set; }

        /// <inheritdoc />
        public Task<bool> RequestAsync(params string[] permissions)
        {
            this.GenerateRequestId();

            var newTcs = new TaskCompletionSource<bool>(this.CurrentRequestId);

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
                var permissionsToRequest = permissions.Where(permission => !this.CheckPermissionGranted(permission)).ToList();

                if (permissionsToRequest.Count > 0)
                {
                    ActivityCompat.RequestPermissions(this.CurrentActivity, permissionsToRequest.ToArray(), this.CurrentRequestId);
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