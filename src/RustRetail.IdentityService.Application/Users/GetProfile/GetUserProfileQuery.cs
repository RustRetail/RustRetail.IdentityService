using RustRetail.IdentityService.Contracts.Users.GetProfile;
using RustRetail.SharedApplication.Abstractions;

namespace RustRetail.IdentityService.Application.Users.GetProfile
{
    public record GetUserProfileQuery() : IQuery<GetUserProfileResponse>;
}
