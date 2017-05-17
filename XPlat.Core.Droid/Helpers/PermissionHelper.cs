using System.Linq;
using Android.Content.PM;

namespace XPlat.Core.Droid.Helpers
{
    public static class PermissionHelper
    {
        public static bool VerifyPermissions(Permission[] grantResults)
        {
            // Verify that each required permission has been granted, otherwise return false.
            return grantResults.Length >= 1 && grantResults.All(result => result == Permission.Granted);
        }
    }
}