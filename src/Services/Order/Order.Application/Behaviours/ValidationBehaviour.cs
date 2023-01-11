using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Behaviours
{
    /*
     https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs0314?f1url=%3FappId%3Droslyn%26k%3Dk(CS0314) 
    => Generic tanımlı bir sınıf eğer tip kısıtı olan bir nesne ile çalışıyor ise, aynı kısıtı kendisinde de uygulamalı. Burada IPipelineBehavior interface' inde TRequest için tip kısıtlaması bulunuyor. Bu sebeple bu interface'den türeyen tüm sınıflar da bu kısıtlamayı uygulamalı.
     */
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
