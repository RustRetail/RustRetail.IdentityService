using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Application.Users.GetProfileById;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Enums;
using RustRetail.IdentityService.Domain.Errors.Authentication;
using RustRetail.IdentityService.Domain.Errors.User;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Abstractions;
using RustRetail.SharedKernel.Domain.Enums;

namespace RustRetail.IdentityService.Application.Users.CreateOrUpdateProfile
{
    internal class CreateOrUpdateUserProfileCommandHandler(
        IIdentityUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
        : ICommandHandler<CreateOrUpdateUserProfileCommand>
    {
        readonly IUserRepository _userRepository = unitOfWork.GetRepository<IUserRepository>();

        public async Task<Result> Handle(CreateOrUpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            if (!currentUserService.IsAuthenticated || string.IsNullOrWhiteSpace(currentUserService.UserId))
            {
                return Result.Failure(AuthenticationErrors.UserNotAuthenticated);
            }

            var user = await _userRepository.GetAsync(new GetUserProfileByIdSpecification(Guid.Parse(currentUserService.UserId), true));
            if (user is null)
            {
                return Result.Failure(UserErrors.UserNotFoundWithId);
            }
            user.CreateOrUpdateProfile(UserProfile.Create(user.Id,
                    request.FirstName,
                    request.LastName,
                    Enumeration.FromName<Gender>(request.Gender!),
                    request.Bio,
                    request.DateOfBirth));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
