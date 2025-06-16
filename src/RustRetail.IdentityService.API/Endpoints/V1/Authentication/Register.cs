using MediatR;
using Microsoft.AspNetCore.Mvc;
using RustRetail.IdentityService.API.Common;
using RustRetail.IdentityService.Application.Authentication.Register;
using RustRetail.IdentityService.Contracts.Authentication.Register;
using RustRetail.SharedInfrastructure.MinimalApi;

namespace RustRetail.IdentityService.API.Endpoints.V1.Authentication
{
    public class Register : IEndpoint
    {
        const string Route = $"{Resource.Authentication}/register";

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost(Route, Handle)
                .WithTags(Tags.Authentication)
                .AllowAnonymous()
                .MapToApiVersion(1)
                .AddEndpointFilter<ValidationFilter<RegisterRequest>>();
        }

        static async Task<IResult> Handle(
            [FromBody] RegisterRequest? request,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(
                        request!.Email,
                        request.Password,
                        request.ConfirmPassword,
                        request.UserName);
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess
                ? Results.Ok(new SuccessResultWrapper(result, httpContext))
                : ResultExtension.HandleFailure(result, httpContext);
        }
    }
}
