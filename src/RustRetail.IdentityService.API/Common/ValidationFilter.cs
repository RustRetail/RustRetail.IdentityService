using FluentValidation;
using RustRetail.IdentityService.Domain.Errors.Common;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.API.Common
{
    public class ValidationFilter<TRequest> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<TRequest>>();
            if (validator is null)
                return await next(context);

            var request = context.Arguments.OfType<TRequest>().FirstOrDefault();
            if (request is null)
                return ResultExtension.HandleFailure(
                    Result.Failure(ValidationErrors.RequestBodyMissing),
                    context.HttpContext);

            var validationResult = await validator.ValidateAsync(request);
            var validationErrors = validationResult.Errors.Where(validationFailure => validationFailure is not null)
                .Select(failure => new { Field = failure.PropertyName, Description = failure.ErrorMessage })
                .Distinct()
                .ToArray();
            var error = Error.Validation(ValidationErrors.InvalidRequest.Code,
                ValidationErrors.InvalidRequest.Description,
                validationErrors);
            if (!validationResult.IsValid && validationErrors.Any())
            {
                return ResultExtension.HandleFailure(
                    Result.Failure(error),
                    context.HttpContext);
            }

            return await next(context);
        }
    }
}
