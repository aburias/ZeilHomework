using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using zh.application.Queries.ValidateCreditCard;

namespace zh.api.Endpoints;

public static class CreditcardEndpoints
{
    public static WebApplication AddCreditcardEndpoints(this WebApplication app)
    {
        app.MapGet("api/v1/creditcard/validate", async (
                [FromQuery] string creditCardNumber,
                IMediator mediator,
                IValidator<ValidateCreditCardQuery> validator) =>
            {
                // Validate input
                var query = new ValidateCreditCardQuery(creditCardNumber);
                var validationResult = await validator.ValidateAsync(query);

                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }

                // Process the query through MediatR
                var isValid = await mediator.Send(query);
                return Results.Ok(new { IsValid = isValid });
            })
            .WithName("ValidateCreditCard")
            .WithOpenApi();

        return app;
    }
}