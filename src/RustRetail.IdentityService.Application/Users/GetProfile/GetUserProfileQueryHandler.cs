using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Application.Users.GetProfileById;
using RustRetail.IdentityService.Contracts.Users.GetProfile;
using RustRetail.IdentityService.Domain.Errors.Authentication;
using RustRetail.IdentityService.Domain.Errors.User;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Users.GetProfile
{
    internal class GetUserProfileQueryHandler(
        IIdentityUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
        : IQueryHandler<GetUserProfileQuery, GetUserProfileResponse>
    {
        readonly IUserRepository _userRepository = unitOfWork.GetRepository<IUserRepository>();

        public async Task<Result<GetUserProfileResponse>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            if (!currentUserService.IsAuthenticated || string.IsNullOrWhiteSpace(currentUserService.UserId))
            {
                return Result.Failure<GetUserProfileResponse>(AuthenticationErrors.UserNotAuthenticated);
            }

            var user = await _userRepository.GetAsync(new GetUserProfileByIdSpecification(Guid.Parse(currentUserService.UserId)), cancellationToken);

            if (user is null)
            {
                return Result.Failure<GetUserProfileResponse>(UserErrors.UserNotFoundWithId);
            }

            // If user have not create a profile yet
            // Return empty profile
            if (user.Profile is null)
            {
                return Result.Success(GetUserProfileResponse.Empty(user.Id));
            }
            var response = new GetUserProfileResponse(
                user.Id,
                user.Profile!.FirstName,
                user.Profile.LastName,
                user.Profile.Gender?.Name,
                user.Profile.AvatarUrl,
                user.Profile.Bio,
                user.Profile.DateOfBirth
            );

            return Result.Success(response);
        }
    }
}
