using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Exceptions;
using MediatR;

namespace Application.Common.Behaviours
{
    public class DomainExceptionHandlerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public DomainExceptionHandlerBehaviour() { }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (DomainException ex)
            {
                throw new ApplicationMessageException($"DomainException Occured, With Code {ex.Code}");
            }
        }
    }
}
