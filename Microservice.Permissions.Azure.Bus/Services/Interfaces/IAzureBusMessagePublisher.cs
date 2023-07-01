using Microservice.Permissions.Azure.Bus.Contracts;

namespace Microservice.Permissions.Azure.Bus.Services.Interfaces;

public interface IAzureBusMessagePublisher
{
    Task PublishMessage<T>(BusMessage<T> message);
}