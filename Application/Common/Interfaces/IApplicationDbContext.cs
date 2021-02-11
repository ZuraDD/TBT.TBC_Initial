using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbContext GetContext();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
