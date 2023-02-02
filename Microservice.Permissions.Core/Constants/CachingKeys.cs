namespace Microservice.Permissions.Core.Constants;

public static class CachingKeys
{
    public const string Area = "areas:{0}";
    public const string Areas = "applications:{0}:areas";
    public const string AreasRolesPermissions = "areas:{0}:roles:{1}";
    public const string ApplicationAreaRolePermissions = "applications:{0}:areas:{1}:roles:{2}";
}