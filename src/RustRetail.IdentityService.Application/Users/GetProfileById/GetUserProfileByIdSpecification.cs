using RustRetail.IdentityService.Domain.Entities;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Users.GetProfileById
{
    internal class GetUserProfileByIdSpecification : Specification<User, Guid>
    {
        public GetUserProfileByIdSpecification(Guid userId, bool asTracking = false)
            : base(u => u.Id == userId)
        {
            AsTracking = asTracking;
            AddInclude(u => u.Profile);
        }
    }
}
