
# **Credit Card Validation API**

This project implements a **Minimal API** in **.NET 8** for validating credit card numbers using the **Luhn algorithm**, following **Clean Architecture** principles. It is structured into multiple projects for scalability and maintainability, leveraging **MediatR for CQRS**, **FluentValidation**, and comprehensive unit testing.

---

## **Features**

- **Luhn Algorithm Validation**:
  Validate credit card numbers based on the Luhn checksum.
- **Clean Architecture**:
  Separates concerns into `API`, `Application`, `Domain`, and `Infrastructure` layers.
- **MediatR**:
  Implements the CQRS pattern for handling queries.
- **FluentValidation**:
  Handles input validation with descriptive error messages.
- **Unit Testing**:
  Covers validators, handlers, and domain logic with automated tests.

---

## **Project Structure**

```plaintext
Solution 'ZeilHomework'
├── presentation/
│   ├── zh.api/                        # API Layer
│   │   ├── Endpoints/
│   │   │   ├── CreditcardEndpoints.cs # API endpoints
│   │   ├── Program.cs                 # Main API configuration
├── src/
│   ├── zh.application/                # Application Layer
│   │   ├── Queries/
│   │   │   ├── ValidateCreditCard/
│   │   │   │   ├── ValidateCreditCardQuery.cs        # MediatR Query
│   │   │   │   ├── ValidateCreditCardQueryHandler.cs # Query Handler
│   │   ├── Validators/
│   │       ├── ValidateCreditCardQueryValidator.cs   # FluentValidation
│   ├── zh.domain/                     # Domain Layer
│   │   ├── Entities/
│   │   │   ├── CreditCard.cs          # Core business logic
│   ├── zh.infrastructure/             # Infrastructure Layer
│   │   ├── DependencyInjection.cs     # Dependency injection
│   │   ├── ExternalPackageDI.cs       # 3rd-party package configurations
├── tests/
│   ├── zh.tests/                      # Test Project
│   │   ├── CreditCardTests.cs         # Domain tests
│   │   ├── ValidateCreditCardHandlerTests.cs # MediatR Handler tests
│   │   ├── ValidateCreditCardValidatorTests.cs # Validator tests
```

---

## **Technologies Used**

- **.NET 8 Minimal API**
- **MediatR (CQRS)**
- **FluentValidation**
- **xUnit** for testing
- **Moq** for mocking dependencies
- **Swashbuckle** for API documentation

---

## **API Documentation**

### **Endpoint**
`GET /api/v1/creditcard/validate`

### **Query Parameters**
- `creditCardNumber`: A string representing the credit card number to validate.

### **Example Request**
```http
GET /api/v1/creditcard/validate?creditCardNumber=4539148803436467
```

### **Responses**

#### **200 OK**
```json
{
  "isValid": true
}
```

#### **400 Bad Request**
```json
[
  "Credit card number cannot be empty.",
  "Credit card number must be numeric.",
  "Credit card number is not valid according to the Luhn algorithm."
]
```

### **Swagger Documentation**
The API documentation is available at:

[https://localhost:7215/swagger/index.html](https://localhost:7215/swagger/index.html)

---

## **Setup and Installation**

1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   cd <repository-folder>
   ```

2. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the Solution**:
   ```bash
   dotnet build
   ```

4. **Run the Application**:
   ```bash
   dotnet run --project presentation/zh.api
   ```

5. **Access the Swagger Documentation**:
   Navigate to: [https://localhost:7215/swagger/index.html](https://localhost:7215/swagger/index.html)

---

## **Clean Architecture Overview**

This project is organized according to **Clean Architecture** principles:

1. **Presentation Layer**:
   - Handles HTTP requests and responses.
   - Defines API endpoints (e.g., `CreditcardEndpoints.cs`).

2. **Application Layer**:
   - Implements CQRS using `MediatR`.
   - Contains query definitions and handlers.
   - Includes input validation using `FluentValidation`.

3. **Domain Layer**:
   - Encapsulates core business rules in the `CreditCard` entity.
   - Implements Luhn algorithm validation directly in the domain.

4. **Infrastructure Layer**:
   - Handles dependency injection and cross-cutting concerns.

---

## **Unit Tests**

1. **Run All Tests**:
   ```bash
   dotnet test
   ```

2. **Test Coverage**:
   - **Validators**:
     Ensures input validation rules are correctly applied.
   - **Handlers**:
     Verifies the query handler processes requests correctly.
   - **Domain Logic**:
     Ensures the `CreditCard` entity implements the Luhn algorithm accurately.

---

## **Code Highlights**

### **API Endpoint (CreditcardEndpoints.cs)**
```csharp
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
```

---

## **Future Improvements**

1. **Support for Additional Validation Rules**:
   - Add issuer-specific rules for credit cards (e.g., BIN checks).
2. **Integration with Databases**:
   - Store and retrieve validation results for auditing.
3. **Advanced Error Handling**:
   - Implement custom exception middleware for detailed error responses.

---

## **License**

This project is licensed under the MIT License.
