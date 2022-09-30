﻿namespace Microservice.Permissions.Core.Contracts.Requests.Area
{
    public class UpdateAreaRequest
    {
        public int ApplicationId { get; }
        public string Name { get; }

        public UpdateAreaRequest(string name, int applicationId)
        {
            Name = name;
            ApplicationId = applicationId;
        }
    }
}