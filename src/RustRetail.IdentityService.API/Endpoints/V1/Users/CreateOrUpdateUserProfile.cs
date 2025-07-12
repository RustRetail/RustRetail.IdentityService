using MediatR;
using Microsoft.AspNetCore.Mvc;
using RustRetail.IdentityService.API.Common;
using RustRetail.IdentityService.Application.Users.CreateOrUpdateProfile;
using RustRetail.IdentityService.Contracts.Users.CreateOrUpdateProfile;
using RustRetail.SharedInfrastructure.MinimalApi;

namespace RustRetail.IdentityService.API.Endpoints.V1.Users
{
    public class CreateOrUpdateUserProfile : IEndpoint
    {
        const string Route = $"{Resource.Users}/profile";

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost(Route, Handle)
                .WithTags(Tags.Users)
                .RequireAuthorization()
                .AddEndpointFilter<ValidationFilter<CreateOrUpdateUserProfileRequest>>()
                .MapToApiVersion(1);
        }

        static async Task<IResult> Handle(
            [FromBody] CreateOrUpdateUserProfileRequest? request,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken)
        {
            DateTime? parsedDateOfBirth = null;
            if (!string.IsNullOrWhiteSpace(request!.DateOfBirth))
            {
                var dt = DateTime.Parse(request.DateOfBirth);
                parsedDateOfBirth = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            }

            var command = new CreateOrUpdateUserProfileCommand(
                        request.FirstName,
                        request.LastName,
                        request.Gender,
                        request.Bio,
                        parsedDateOfBirth);
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess
                ? Results.Ok(new SuccessResultWrapper(result, httpContext))
                : ResultExtension.HandleFailure(result, httpContext);
        }
    }
}
