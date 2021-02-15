using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Events.PersonEvents;
using Newtonsoft.Json;

namespace Application.PersonController.EventHandlers
{
    public class CreatePersonEventHandler : INotificationHandler<DomainEventNotification<PersonCreatedEvent>>
    {
        private readonly ILogger<CreatePersonEventHandler> _logger;

        public CreatePersonEventHandler(ILogger<CreatePersonEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<PersonCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent} Handled", domainEvent.GetType().Name);

            var info = JsonConvert.SerializeObject(notification.DomainEvent, Formatting.None,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            _logger.LogInformation($"Info - {info}");

            return Task.CompletedTask;
        }
    }
}
