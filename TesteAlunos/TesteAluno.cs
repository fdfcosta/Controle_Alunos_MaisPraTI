using Xunit;
using Alunos.Entities;

namespace TesteAlunos
{
    public class TesteAluno
    {
        [Fact]
        public void TesteAlteraNome()
        {
            //testando side-effect causado pelo método AlteraNome() utilizando estrutura AAA

            //Arrange
            Aluno Aluno = new();
            string Nome = "Fabrício";


            //Act
            Aluno.AlteraNome(Nome);

            //Assert
            Assert.Equal(Nome, Aluno.Nome);

        }

        [Fact]
        public void TesteAlteraIdade()
        {
            Aluno Aluno = new();
            int Idade = 20;

            Aluno.AlteraIdade(Idade);

            Assert.Equal(Idade, Aluno.Idade);
        }

        [Fact]
        public void TesteAlteraEmail()
        {
            Aluno aluno = new();
            string Email = "devcode.fabricio@gmail.com";

            aluno.AlteraEmail(Email);

            Assert.Equal(Email, aluno.Email);

        }
    }


}