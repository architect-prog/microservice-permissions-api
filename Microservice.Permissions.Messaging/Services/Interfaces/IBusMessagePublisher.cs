using Microservice.Permissions.Messaging.Contracts;

namespace Microservice.Permissions.Messaging.Services.Interfaces;

public interface IBusMessagePublisher
{
    Task PublishMessage<T>(BusMessage<T> message);
}