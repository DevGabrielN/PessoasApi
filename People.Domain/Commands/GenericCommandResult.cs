using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace People.Domain.Commands;
public class GenericCommandResult<T>
{    
    public bool Success { get; private set; }
    public string Date { get; private set; }
    public string Message { get; private set; }
    public T? Data { get; private set; }
    private List<ValidationError?>? _errors { get; set; }
    public IReadOnlyCollection<ValidationError?>? Errors { get { return _errors?.ToArray() ?? null; } }

    public partial class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public GenericCommandResult(
        bool success = true,
        string message = "Concluído",
        T? data = default,
        List<ValidationFailure>? errorsFluent = default,
        List<IdentityError>? errorsIdentity = default,
        string? propertyName = default,
        string? errorMessage = default)
    {
        Success = success;
        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Message = message;
        Data = data;

        _errors = new List<ValidationError>();

        if (errorsFluent != null)
            _errors.AddRange(errorsFluent.Select(x => new ValidationError { PropertyName = x.PropertyName, ErrorMessage = x.ErrorMessage }));

        if (errorsIdentity != null)
            _errors.AddRange(errorsIdentity.Select(x => new ValidationError { PropertyName = "User", ErrorMessage = x.Description }));

        if (!string.IsNullOrEmpty(propertyName) && !string.IsNullOrEmpty(errorMessage))        
            _errors.Add(new ValidationError { PropertyName = propertyName, ErrorMessage = errorMessage });        
    }
}
