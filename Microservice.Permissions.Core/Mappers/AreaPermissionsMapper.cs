﻿using ArchitectProg.FunctionalExtensions.Extensions;
using ArchitectProg.Kernel.Extensions.Abstractions;
using Microservice.Permissions.Core.Constants;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers
{
    public class AreaPermissionsMapper :
        Mapper<AreaRolePermissionsEntity, PermissionsResponse>,
        IAreaPermissionsMapper
    {
        private readonly IPermissionMapper permissionMapper;

        public AreaPermissionsMapper(IPermissionMapper permissionMapper)
        {
            this.permissionMapper = permissionMapper;
        }

        public override PermissionsResponse Map(AreaRolePermissionsEntity source)
        {
            var permissions = source.Permissions?.ToArray() ?? Array.Empty<PermissionEntity>();
            var defaultPermissions = permissions
                .Where(x => PermissionConstants.Defaults.Any(y => x.Name.EqualsIgnoreCase(y)))
                .ToArray();

            var customPermissions = permissions
                .Except(defaultPermissions)
                .ToArray();

            var canReadPermission = defaultPermissions
                .FirstOrDefault(x => x.Name.EqualsIgnoreCase(PermissionConstants.CanRead));
            var canCreatePermission = defaultPermissions
                .FirstOrDefault(x => x.Name.EqualsIgnoreCase(PermissionConstants.CanCreate));
            var canUpdatePermission = defaultPermissions
                .FirstOrDefault(x => x.Name.EqualsIgnoreCase(PermissionConstants.CanUpdate));
            var canDeletePermission = defaultPermissions
                .FirstOrDefault(x => x.Name.EqualsIgnoreCase(PermissionConstants.CanDelete));

            var result = new PermissionsResponse
            {
                Id = source.Id,
                RoleId = source.RoleId,
                AreaId = source.AreaId,
                CanRead = canReadPermission?.HaveAccess ?? false,
                CanCreate = canCreatePermission?.HaveAccess ?? false,
                CanUpdate = canUpdatePermission?.HaveAccess ?? false,
                CanDelete = canDeletePermission?.HaveAccess ?? false,
                CustomPermissions = permissionMapper.MapCollection(customPermissions)
            };

            return result;
        }
    }
}