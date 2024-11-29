using MediatR;
using zh.domain.Entities;

namespace zh.application.Queries.ValidateCreditCard;

public class ValidateCreditCardQueryHandler : IRequestHandler<ValidateCreditCardQuery, bool>
{
    public Task<bool> Handle(ValidateCreditCardQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var creditCard = new CreditCard(request.CreditCardNumber);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}