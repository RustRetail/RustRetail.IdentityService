using MediatR;
using RustRetail.IdentityService.API.Common;
using RustRetail.IdentityService.Application.Users.GetProfile;
using RustRetail.IdentityService.Contracts.Users.GetProfile;
using RustRetail.SharedInfrastructure.MinimalApi;

namespace RustRetail.IdentityService.API.Endpoints.V1.Users
{
    public class GetUserProfile : IEndpoint
    {
        const string Route = $"{Resource.Users}/profile";

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(Route, Handle)
                .WithTags(Tags.Users)
                .RequireAuthorization()
                .MapToApiVersion(1);
        }

        static async Task<IResult> Handle(
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var query = new GetUserProfileQuery();
            var result = await sender.Send(query, cancellationToken);
            return result.IsSuccess
                ? Results.Ok(new SuccessResultWrapper<GetUserProfileResponse>(result, httpContext))
                : ResultExtension.HandleFailure(result, httpContext);
        }
    }
}
