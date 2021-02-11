using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class DomainEventHandlerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public DomainEventHandlerBehaviour() { }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (DomainException ex)
            {
                throw new ApplicationMessageException($"DomainException {ex.GetType().Name} Occured, With Code {ex.Code}");
            }
        }
    }
}
