using FluentValidation;
using MediatR;
using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Behaviours;
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IValidator<TRequest> _validator;

    public ValidationPipelineBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationErrorException(
                validationResult.Errors.Select(x => new ValidationError(
                    x.PropertyName, x.ErrorMessage)).ToList());
        }
        return next();
    }
}
