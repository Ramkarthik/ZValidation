# ZValidation

Simple, fast, and extensible validation library for .NET.

### Example

```csharp
using ZValidation;

var user = new Users();

// Initialize zvalidation
var validation = new ZValidation<Users>();

// Add the validations
validation.For(x => x.FirstName).Required();
validation.For(x => x.LastName).Required().LengthMax(100);

// Check the validation response
bool isSuccess = validation.IsSuccessful;
ZResponse response = validation.Response;
List<string> = validation.Response.Errors;

```
