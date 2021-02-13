using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
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
            var person = _context.Person.SingleOrDefault(x => x.Id == request.Id);

            if (person == default(Person))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonNotFound);
            }

            var fileName = _photoUploadService.GetUniqueFileName(request.ProfileImage.FileName);

            var absoluteFilePath = _photoUploadService.GetAbsoluteUploadFilePathForPerson(person.Id, fileName);

            var relativeFilePath = _photoUploadService.GetRelativeUploadFilePathForPerson(person.Id, fileName);

            _photoUploadService.CleanDirectory(person.Id);

            await using (var fileStream = new FileStream(absoluteFilePath, FileMode.Create))
            {
                await request.ProfileImage.CopyToAsync(fileStream, cancellationToken);
            }

            person.RelativeImagePath = relativeFilePath;

            _context.Person.Update(person);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
