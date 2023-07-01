using Microsoft.Extensions.Hosting;

namespace Microservice.Permissions.Messaging.Services.Interfaces;

public interface IMessageBusInitializer : IHostedService, IDisposable
{
}