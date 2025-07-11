using RustRetail.IdentityService.Contracts.Users.GetProfileById;
using RustRetail.SharedApplication.Abstractions;

namespace RustRetail.IdentityService.Application.Users.GetProfileById
{
    public record GetUserProfileByIdQuery(
        Guid UserId)
        : IQuery<GetUserProfileByIdResponse>;
}
