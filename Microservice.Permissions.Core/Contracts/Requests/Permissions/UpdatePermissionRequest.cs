﻿namespace Microservice.Permissions.Core.Contracts.Requests.Permissions
{
    public class UpdatePermissionRequest
    {
        public string Name { get; }
        public bool HasAccess { get; }

        public UpdatePermissionRequest(string name, bool hasAccess)
        {
            Name = name;
            HasAccess = hasAccess;
        }
    }
}