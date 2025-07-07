using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Domain.Repositories;

namespace RustRetail.IdentityService.Infrastructure.ApplicationServices
{
    internal class RoleService(
        IIdentityUnitOfWork unitOfWork)
        : IRoleService
    {
        readonly IRoleRepository _roleRepository = unitOfWork.GetRepository<IRoleRepository>();

        public async Task<List<string>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return (await _roleRepository.GetRolesByUserIdAsync(userId, cancellationToken))
                .Select(r => r.NormalizedName)
                .ToList();
        }
    }
}
