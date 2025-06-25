using MyDocs.Shared.Abstracts.Users;
using MyDocs.Shared.Validators;

namespace MyDocs.Features.Users.UpdateUser
{
    public class UpdateUserRequest : BaseRequestGetUser
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }


        public override void ValidateProperties()
        {
            base.ValidateProperties();

            ExceptionValidator.When(string.IsNullOrWhiteSpace(Name), "Informe um nome válido");
            ExceptionValidator.When(string.IsNullOrWhiteSpace(CPF), "Informe um CPF válido");
            ExceptionValidator.When(DateOfBirth == default, "Informe uma data de nascimento válida");
            ExceptionValidator.When(DateOfBirth > DateTime.Now, "Data de nascimento não pode ser futura");
            ExceptionValidator.When(string.IsNullOrWhiteSpace(Phone), "Informe um telefone válido");
        }
    }
}
