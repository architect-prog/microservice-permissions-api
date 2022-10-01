namespace Microservice.Permissions.Core.Constants;

public static class PermissionConstants
{
    public const string CanRead = "can_read";
    public const string CanCreate = "can_create";
    public const string CanUpdate = "can_update";
    public const string CanDelete = "can_delete";

    public static readonly string[] Defaults =
    {
        CanRead, CanCreate, CanUpdate, CanDelete
    };
}