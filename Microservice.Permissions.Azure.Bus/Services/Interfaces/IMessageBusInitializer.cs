using Microsoft.Extensions.Hosting;

namespace Microservice.Permissions.Azure.Bus.Services.Interfaces;

public interface IMessageBusInitializer : IHostedService, IAsyncDisposable
{
}