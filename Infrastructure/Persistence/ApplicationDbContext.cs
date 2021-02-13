using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public DbSet<City> City { get; set; }

        public DbSet<GenderType> GenderType { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<PhoneNumber> PhoneNumber { get; set; }

        public DbSet<PhoneNumberType> PhoneNumberType { get; set; }

        public DbSet<Relation> Relation { get; set; }

        public DbSet<RelationType> RelationType { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public DbContext GetContext()
        {
            return this;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Auditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            var domainEvents = ChangeTracker
                .Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .OrderBy(x => x.DateOccurred)
                .Where(domainEvent => !domainEvent.IsPublished).ToList();

                if (!domainEvents.Any()) return;

                foreach (var domainEvent in domainEvents)
                {
                    domainEvent.IsPublished = true;

                    await _domainEventService.Publish(domainEvent);
                }
        }
    }
}
