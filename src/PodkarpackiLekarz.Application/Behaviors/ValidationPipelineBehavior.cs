using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PodkarpackiLekarz.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace PodkarpackiLekarz.Application.Behaviours;
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationPipelineBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {            
            var validator = scope.ServiceProvider.GetService<IValidator<TRequest>>();

            if (validator is null)
                return next();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationErrorException(
                    validationResult.Errors.Select(x => new ValidationError(
                        x.PropertyName, x.ErrorMessage)).ToList());
            }
            return next();
        }        
    }
}
