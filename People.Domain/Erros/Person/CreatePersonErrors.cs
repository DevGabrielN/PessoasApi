namespace People.Domain.Erros.Person;

public class CreatePersonErrors
{
    #region DateBirth
    public const string DateBirthRequired = "A data de nascimento é obrigatória";

    public const string DateBirthMustBeLessOrEqualToNow = "A data de nascimento deve ser menor ou igual à data atual";

    public const string DateBirthMustBeWithin150Years = "A data de nascimento deve estar dentro dos últimos 150 anos";
    #endregion
    #region CPF
    public const string InvalidCPF = "CPF inválido";

    public const string CPFRequired = "O CPF é obrigatório";
    #endregion
    #region Name
    public const string Required = "O campo nome é obrigatório";

    public const string FirstNameRequired = "O primeiro nome é obrigatório";

    public const string LastNameRequired = "O sobrenome é obrigatório";

    public const string FirstNameLengthRequirement = "O primeiro nome deve ter conter entre 3 e 150 caracteres";

    public const string LastNameLengthRequirement = "O sobrenome deve ter conter entre 3 e 150 caracteres";

    public const string InvalidFirstName = "Formato do primeiro nome inválido";

    public const string InvalidLastName = "Formato do sobrenome inválido";
    #endregion
    #region UF
    public const string UFRequired = "UF é obrigatório";

    public const string InvalidUF = "UF inválida";
    #endregion
}
