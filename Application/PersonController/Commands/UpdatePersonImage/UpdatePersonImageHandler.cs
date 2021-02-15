using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.PersonController.Commands.UpdatePersonImage
{
    public class UpdatePersonImageHandler : IRequestHandler<UpdatePersonImageCommand>
    {
        private readonly IApplicationDbContext _context;

        private readonly ILogger<UpdatePersonImageHandler> _logger;

        private readonly IPhotoUploadService _photoUploadService;

        public UpdatePersonImageHandler(IApplicationDbContext context, ILogger<UpdatePersonImageHandler> logger, IPhotoUploadService photoUploadService)
        {
            _context = context;
            _logger = logger;
            _photoUploadService = photoUploadService;
        }

        public async Task<Unit> Handle(UpdatePersonImageCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Person.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (person == default(Person))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonNotFound);
            }

            person.ImagePath = await _photoUploadService.UplodPhotoAndReturnRelativePath(request.ProfileImage, person.Id, cancellationToken);

            _context.Person.Update(person);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
