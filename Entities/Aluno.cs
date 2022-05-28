using System.Text;

namespace Alunos.Entities
{
    public class Aluno
    {
        public string Nome{ get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }

        public Aluno() { }

        public Aluno(string Nome, int Idade, string Email)
        {
            this.Nome = Nome;
            this.Idade = Idade;
            this.Email = Email;
        }

        public void AlteraNome(string NovoNome)
        {
            Nome = NovoNome;
        }

        public void AlteraIdade(int NovaIdade)
        {
            Idade = NovaIdade;
        }
    
        public void AlteraEmail(string NovoEmail)
        {
            Email = NovoEmail;
        }

        public override string ToString()
        {
            return $"Nome: {Nome} - Idade: {Idade} - E-mail: {Email}";

        }
    }
}
