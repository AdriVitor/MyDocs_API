using MyDocs.Shared.Abstracts.Validators;
using MyDocs.Shared.Validators;

namespace MyDocs.Features.Users.Create
{
    public class CreateUserRequest : ValidatorBaseAbstract
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }

        public override void ValidateProperties()
        {
            ExceptionValidator.When(string.IsNullOrWhiteSpace(Name), "Informe o nome do usuário");

            ExceptionValidator.When(string.IsNullOrWhiteSpace(Email), "Informe o e-mail");
            ExceptionValidator.When(!Email.Contains("@") || !Email.Contains("."), "Informe um e-mail válido");

            ExceptionValidator.When(string.IsNullOrWhiteSpace(Password), "Informe a senha");
            ExceptionValidator.When(Password?.Length < 6, "A senha deve conter pelo menos 6 caracteres");

            ExceptionValidator.When(string.IsNullOrWhiteSpace(CPF), "Informe o CPF");
            ExceptionValidator.When(CPF?.Length != 11 || !CPF.All(char.IsDigit), "Informe um CPF válido com 11 dígitos numéricos");

            ExceptionValidator.When(DateOfBirth == default, "Informe a data de nascimento");
            ExceptionValidator.When(DateOfBirth > DateTime.Today, "A data de nascimento não pode ser futura");

            ExceptionValidator.When(string.IsNullOrWhiteSpace(Phone), "Informe o telefone");
        }
    }
}
