using MediatR;

namespace zh.application.Queries.ValidateCreditCard;

public record ValidateCreditCardQuery(string CreditCardNumber) : IRequest<bool>;