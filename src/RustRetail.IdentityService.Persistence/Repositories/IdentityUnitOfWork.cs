using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.IdentityService.Persistence.Database;
using RustRetail.SharedPersistence.Database;

namespace RustRetail.IdentityService.Persistence.Repositories
{
    internal class IdentityUnitOfWork : UnitOfWork<IdentityDbContext>, IIdentityUnitOfWork
    {
        public IdentityUnitOfWork(IdentityDbContext context, IServiceProvider serviceProvider)
            : base(context, serviceProvider)
        {
        }
    }
}
