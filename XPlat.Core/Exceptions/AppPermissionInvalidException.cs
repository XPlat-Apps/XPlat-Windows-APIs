namespace XPlat.Exceptions
{
    using System;

    public class AppPermissionInvalidException : Exception
    {
        public AppPermissionInvalidException(string permission, string message)
            : this(permission, message, null)
        {
        }

        public AppPermissionInvalidException(string permission, string message, Exception innerException)
            : base(message, innerException)
        {
            this.PermissionCode = permission;
        }

        public string PermissionCode { get; }
    }
}