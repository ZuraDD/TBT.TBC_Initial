using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<City> City { get; set; }

        DbSet<GenderType> GenderType { get; set; }

        DbSet<Person> Person { get; set; }

        DbSet<PhoneNumber> PhoneNumber { get; set; }

        DbSet<PhoneNumberType> PhoneNumberType { get; set; }

        DbSet<Relation> Relation { get; set; }

        DbSet<RelationType> RelationType { get; set; }

        DbContext GetContext();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
