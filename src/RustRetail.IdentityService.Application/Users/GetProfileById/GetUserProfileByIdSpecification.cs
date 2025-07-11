using RustRetail.IdentityService.Domain.Entities;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Users.GetProfileById
{
    internal class GetUserProfileByIdSpecification : Specification<User, Guid>
    {
        public GetUserProfileByIdSpecification(Guid userId)
            : base(u => u.Id == userId)
        {
            AsTracking = false;
            AddInclude(u => u.Profile);
        }
    }
}
