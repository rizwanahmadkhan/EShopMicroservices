
using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System.Windows.Input;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(validators.Select(val => val.ValidateAsync(context, cancellationToken)));

            var failures = validationResults.Where(err => err.Errors.Any()).SelectMany(err => err.Errors).ToList();

            if(failures.Count > 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
