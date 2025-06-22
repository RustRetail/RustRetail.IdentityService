using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Domain.Constants;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Errors.Authentication;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.Register
{
    internal class RegisterCommandHandler(
        IIdentityUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
        : ICommandHandler<RegisterCommand>
    {
        IUserRepository userRepository = unitOfWork.GetRepository<IUserRepository>();
        IRoleRepository roleRepository = unitOfWork.GetRepository<IRoleRepository>();

        public async Task<Result> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            // Check for duplicate email and username
            if (await userRepository.ExistsAsync(
                u => u.NormalizedEmail == request.Email.Trim().ToUpperInvariant(),
                cancellationToken))
            {
                return Result.Failure(RegisterErrors.EmailExisted);
            }
            if (await userRepository.ExistsAsync(
                u => u.NormalizedUserName == request.UserName.Trim().ToUpperInvariant(),
                cancellationToken))
            {
                return Result.Failure(RegisterErrors.UserNameExisted);
            }

            var user = User.RegisterUser(
                request.Email.Trim(),
                request.UserName.Trim(),
                passwordHasher.HashPassword(request.Password.Trim()));
            user.Roles.Add(new UserRole()
            {
                RoleId = (await roleRepository.GetRoleByNameAsync(
                    ApplicationRole.User,
                    cancellationToken: cancellationToken))?.Id
                ?? throw new InvalidOperationException("Role not found.")
            });
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
