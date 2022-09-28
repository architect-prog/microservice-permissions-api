namespace Microservice.Permissions.Core.Constants
{
    public static class PermissionConstants
    {
        public const string CanRead = "can_read";
        public const string CanCreate = "can_read";
        public const string CanUpdate = "can_read";
        public const string CanDelete = "can_read";

        public static readonly string[] Defaults =
        {
            CanRead, CanCreate, CanUpdate, CanDelete
        };
    }
}