using MediatR;
using RustRetail.IdentityService.API.Common;
using RustRetail.IdentityService.Application.Users.GetProfileById;
using RustRetail.IdentityService.Contracts.Users.GetProfileById;
using RustRetail.SharedInfrastructure.MinimalApi;

namespace RustRetail.IdentityService.API.Endpoints.V1.Users
{
    public class GetUserProfileById : IEndpoint
    {
        const string Route = $"{Resource.Users}/{{id:guid}}/profile";

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(Route, Handle)
                .WithTags(Tags.Users)
                .AllowAnonymous()
                .MapToApiVersion(1);
        }

        static async Task<IResult> Handle(
            Guid id,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var query = new GetUserProfileByIdQuery(id);
            var result = await sender.Send(query);
            return result.IsSuccess
                ? Results.Ok(new SuccessResultWrapper<GetUserProfileByIdResponse>(result, httpContext))
                : ResultExtension.HandleFailure(result, httpContext);
        }
    }
}
