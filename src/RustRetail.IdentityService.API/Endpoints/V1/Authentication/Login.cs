using MediatR;
using Microsoft.AspNetCore.Mvc;
using RustRetail.IdentityService.Application.Authentication.Login;
using RustRetail.IdentityService.Contracts.Authentication.Login;
using RustRetail.SharedInfrastructure.MinimalApi;

namespace RustRetail.IdentityService.API.Endpoints.V1.Authentication
{
    public class Login : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{Resource.Authentication}/login", async ([FromBody] LoginRequest request, ISender sender) =>
            {
                // To do: validate the request model
                var loginCommand = new LoginCommand(request.Email, request.Password);
                var result = await sender.Send(loginCommand);
                // To do: create Results extension to map error type to appropriate status code
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithTags(Tags.Authentication)
            .AllowAnonymous()
            .MapToApiVersion(1);
        }
    }
}
