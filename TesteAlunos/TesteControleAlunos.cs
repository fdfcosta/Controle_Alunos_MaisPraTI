using Xunit;
using Alunos.Entities;

namespace TesteAlunos
{
    public class ControleAlunosTeste
    {
        [Fact]
        public void TesteValidaNome()
        {
            //arrange
            ControleAlunos c = new();
            string Nome = "Fabrício Del Frari da Costa";

            //act
            bool resultado = c.ValidaNome(Nome);
            bool esperado = true;

            //Assett
            Assert.Equal(esperado, resultado);

        }

        [Fact]
        public void TesteRetiraAcentosNome()
        {
            //arrange
            ControleAlunos c = new();
            string letraComAcento = "áàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ";

            //act
            string esperado = "aaaaeeeiiooooucnAAAAEEIIOOOOUCN";
            string resultado = c.RetiraAcentosNome(letraComAcento);

            //Assert
            Assert.Equal(esperado, resultado);

        }

        [Fact]
        public void TesteValidaIdade()
        {
            //arrange
            ControleAlunos c = new();
            int IdadeValida = 25;
            int IdadeInvalida = 200;

            //act
            var resultadoValido = c.ValidaIdade(IdadeValida);
            var resultadoInvalido = c.ValidaIdade(IdadeInvalida);

            //Assert
            bool esperadoValido = true;
            bool esperadoInvalido = false;

            Assert.Equal(esperadoValido, resultadoValido);
            Assert.Equal(esperadoInvalido, resultadoInvalido);

        }

        [Fact]
        public void TesteValidaEmail()
        {
            //arrange
            ControleAlunos c = new();
            string emailValido = "devcode.fabricio@gmail.com";
            string parteLocalInvalida = "ds@gmail.com";
            string dominioInvalido = "fabricio@.net";
            string dominioDeTopoInvalido = "fabricio@gmail.açslçldaçld";

            //act
            var resultadoValido = c.ValidaEmail(emailValido);
            var esperadoValido = true;
            var resultadoInvalido = c.ValidaEmail(parteLocalInvalida) == true && c.ValidaEmail(dominioInvalido) == true && c.ValidaEmail(dominioDeTopoInvalido) == true;
            var esperadoInvalido = false;

            //assert
            Assert.Equal(esperadoValido, resultadoValido);
            Assert.Equal(esperadoInvalido, resultadoInvalido);

        }

        [Fact]
        public void TesteAlunoJaExiste()
        {
            //arrange
            ControleAlunos c = new();
            c.ListaAlunos.Clear();
            c.ListaAlunos.Add(new Aluno()
            {
                Nome = "Fabrício Del Frari da Costa",
                Idade = 25,
                Email = "devcode.fabricio@gmail.com"
            });
            string Nome = c.ListaAlunos[0].Nome;
            int Idade = c.ListaAlunos[0].Idade;

            //act
            var esperado = true;
            var resultado = c.AlunoJaExiste(Nome, Idade);

            //assert
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void TesteExisteAlunos()
        {
            //arrange
            ControleAlunos c = new();
            c.ListaAlunos.Clear();
            c.ListaAlunos.Add(new Aluno()
            {
                Nome = "Fabrício Del Frari",
                Idade = 25,
                Email = "devcode.fabricio@gmail.com"
            });
            var existeAlunos = c.ExisteAlunos();

            //act
            var esperado = true;
            var resultado = existeAlunos;

            //assert
            Assert.Equal(esperado, resultado);
        }
    }
}
