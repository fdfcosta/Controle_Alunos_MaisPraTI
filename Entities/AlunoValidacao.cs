using FluentValidation;
using System.Text.RegularExpressions;

namespace Alunos.Entities
{
    public class AlunoValidacao : AbstractValidator<Aluno>
    {
        

        bool ValidaEmail(string Email)
        {

            bool RegexEmailTeste = Regex.IsMatch(Email, @"/([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            bool EmailContem = Email.EndsWith(".com") || Email.EndsWith(".net") || Email.EndsWith(".br");

            //separa o email usando @ como parâmetro
            var SplitEmail = Email.Split('@');

            //todo endereço de e-mail consiste em três elementos: parte local, símbolo '@' e nome do domínio
            //a posicao 0 do array será a parte local do email, a posicao 1 será o domínio
            //a parte local do email precisa conter no mínimo 3 caracteres
            bool ParteLocalValida = SplitEmail[0].Length >= 3;

            return RegexEmailTeste && EmailContem && ParteLocalValida;

        }


         

        public AlunoValidacao()
        {
            RuleFor(x => x.Nome).Length(3, 70).Matches(@"^[\p{L} \.']+$");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Idade).GreaterThan(1).NotEmpty().NotNull();  
        }
    }
}
