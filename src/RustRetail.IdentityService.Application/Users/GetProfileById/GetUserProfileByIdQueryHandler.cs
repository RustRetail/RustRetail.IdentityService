using RustRetail.IdentityService.Contracts.Users.GetProfileById;
using RustRetail.IdentityService.Domain.Errors.User;
using RustRetail.IdentityService.Domain.Errors.User.Profile;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Users.GetProfileById
{
    internal class GetUserProfileByIdQueryHandler(
        IIdentityUnitOfWork unitOfWork)
        : IQueryHandler<GetUserProfileByIdQuery, GetUserProfileByIdResponse>
    {
        readonly IUserRepository _userRepository = unitOfWork.GetRepository<IUserRepository>();

        public async Task<Result<GetUserProfileByIdResponse>> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(new GetUserProfileByIdSpecification(request.UserId), cancellationToken);

            if (user is null)
            {
                return Result.Failure<GetUserProfileByIdResponse>(UserErrors.UserNotFoundWithId);
            }

            // User have not create a profile yet
            if (user.Profile is null)
            {
                return Result.Failure<GetUserProfileByIdResponse>(UserProfileErrors.UserProfileNotCreated);
            }
            var response = new GetUserProfileByIdResponse(
                user.Id,
                user.Profile.FirstName,
                user.Profile.LastName,
                user.Profile.Gender is not null ? user.Profile.Gender.Name : null,
                user.Profile.AvatarUrl,
                user.Profile.Bio,
                user.Profile.DateOfBirth
            );

            return Result.Success(response);
        }
    }
}
