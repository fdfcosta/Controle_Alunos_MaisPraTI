using Alunos.Entities;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Alunos
{
    class Program
    {
        static void Main()
        {
            List<Aluno> ListaAlunos = new()
            {
                new Aluno() { Nome = "JOSÉ CARLOS", Idade = 22, Email = "JOSE.CARLOS@GMAIL.COM" },
                new Aluno() { Nome = "FERNANDO COSTA", Idade = 15, Email = "FERNANDO.COSTA@OUTLOOK.COM" },
                new Aluno() { Nome = "OLAVO FERNANDES", Idade = 19, Email = "FERNANDES.OLAVO@HOTMAIL.COM" },
                new Aluno() { Nome = "DIENIFER SILVA", Idade = 24, Email = "DIENI.SILVA@GMAIL.COM" }
            };

            int Opcao = -1;


            ImprimeMenu();


            void ImprimeMenu()
            {


                while (Opcao != 0)
                {
                    Console.WriteLine(@"CONTROLE DE ALUNOS +PRATI - GRUPO TELECON
------------------------------
1- Cadastrar Aluno
2- Consultar todos os Alunos
3- Alterar Aluno
4- Remover Aluno
5- Listar Alunos maiores de idade
6- Listar Alunos menores de idade
0- Sair");

                    Opcao = DefineOpcaoMenu();


                    switch (Opcao)
                    {
                        case 1:
                            CadastrarAluno();
                            ReiniciaTela();
                            break;
                        
                        case 2:
                            ConsultarAlunos();
                            ReiniciaTela();
                            break;
                        
                        case 3:
                            AlterarDadosAluno();
                            ReiniciaTela();
                            break;
                        
                        case 4:
                            RemoverAluno();
                            ReiniciaTela();
                            break;
                        
                        case 5:
                            ListagemAlunosMaiorIdade();
                            ReiniciaTela();
                            break;
                        
                        case 6:
                            ListagemAlunosMenorIdade();
                            ReiniciaTela();
                            break;
                        
                        case 0:
                            Console.Write("Saindo");
                            Thread.Sleep(400);
                            Console.Write('.');
                            Thread.Sleep(400);
                            Console.Write('.');
                            Thread.Sleep(400);
                            Console.Write('.');
                            break;

                        default:
                            Console.WriteLine("Opção inválida!");
                            break;

                    }
                }
            }

            void CadastrarAluno()
            {
                if (!ConfirmaAcao())
                    return;

                Console.Clear();

                Console.WriteLine(@"CADASTRO DE ALUNOS +PRATI - GRUPO TELECON
");

                string Nome = CadastrarNomeAluno();
                int Idade = CadastrarIdadeAluno();
                string Email = CadastrarEmailAluno();

                ListaAlunos.Add(new Aluno(Nome.ToUpper(), Idade, Email.ToUpper()));

                Console.WriteLine(@$"
Aluno cadastrado!
{ListaAlunos[^1]}");

            }

            string CadastrarNomeAluno()
            {
                string NomeAluno = "";
                try
                {
                    while (!ValidaNome(NomeAluno))
                    {
                        Console.Write("Informe o nome completo do aluno: ");
                        NomeAluno = Console.ReadLine().Trim();

                        if (!ValidaNome(NomeAluno))
                        {
                            Console.WriteLine("Nome inválido. Tecle e tente novamente.");
                            NomeAluno = "";
                            Console.ReadKey();
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                }

                return NomeAluno;
            }

            //método auxiliar para CadastrarNomeAluno()
            bool ValidaNome(string Nome)
            {
                string NomeSemAcento = RetiraAcentosNome(Nome);
                bool RegexNomeTeste = Regex.IsMatch(NomeSemAcento, @"^[a-zA-Z]+[\s]");

                return RegexNomeTeste;
            }

            //método auxiliar para ValidaNome()
            string RetiraAcentosNome(string Nome)
            {
                Nome = Nome.Normalize(NormalizationForm.FormD);

                StringBuilder sb = new();

                for (byte i = 0; i < Nome.Length; i++)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(Nome[i]) != UnicodeCategory.NonSpacingMark)
                        sb.Append(Nome[i]);
                }

                return sb.ToString();
            }

            int CadastrarIdadeAluno()
            {

                int IdadeAluno = 0;
                while (!ValidaIdade(IdadeAluno))
                {
                    try
                    {

                        Console.Write("Informe a idade do aluno: ");
                        IdadeAluno = Convert.ToInt32(Console.ReadLine().Trim());

                        if (!ValidaIdade(IdadeAluno))
                        {
                            Console.WriteLine("Idade inválida. Tecle e tente novamente.");
                            IdadeAluno = 0;
                            Console.ReadKey();
                        }
                    }
                    catch (Exception ex)
                    {
                        IdadeAluno = 0;
                        Console.WriteLine("Idade inválida. Tecle e tente novamente.");
                        Console.ReadKey();
                        continue;

                    }
                }


                return IdadeAluno;
            }

            //método auxiliar para CadastrarIdadeAluno()
            bool ValidaIdade(int Idade)
            {
                return Idade > 0 && Idade <= 60;
            }

            string CadastrarEmailAluno()
            {
                string EmailAluno = "";
                try
                {
                    while (!ValidaEmail(EmailAluno))
                    {

                        Console.Write("Informe o e-mail do aluno: ");
                        EmailAluno = Console.ReadLine().Trim();

                        if (!ValidaEmail(EmailAluno))
                        {
                            Console.WriteLine("E-mail inválido. Tecle e tente novamente.");
                            EmailAluno = "";
                            Console.ReadKey();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                }

                return EmailAluno;
            }

            //método auxiliar para CadastrarEmailAluno()
            bool ValidaEmail(string Email)
            {
                // https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address resposta do usuário Vasya Milovidov;
                // testado em https://regex101.com/
                bool RegexEmailTeste = Regex.IsMatch(Email, @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$");

                bool EmailTerminaCom = Email.EndsWith(".com") || Email.EndsWith(".net") || Email.EndsWith(".br");

                //separa o email usando @ como parâmetro
                var SplitEmail = Email.Split('@');

                //todo endereço de e-mail consiste em três elementos: parte local, símbolo '@' e nome do domínio
                //a posicao 0 do array será a parte local do email
                //a parte local do email precisa conter no mínimo 3 caracteres
                bool ParteLocalValida = SplitEmail[0].Length >= 3;

                return RegexEmailTeste && EmailTerminaCom && ParteLocalValida;
            }

            void ConsultarAlunos()
            {
                Console.Clear();
                Console.WriteLine(@"LISTA DE ALUNOS +PRATI - GRUPO TELECON
");
                ListagemAlunosPosicao();
            }

            void AlterarDadosAluno()
            {
                if (!ConfirmaAcao())
                    return;

                Console.Clear();
                Console.WriteLine(@"ALTERAÇÃO CADASTRAL +PRATI - GRUPO TELECON
");

                if (!ExisteAlunos())
                {
                    Console.WriteLine("Não há alunos cadastrados para alteração.");
                    return;
                }

                int Posicao = DefinePosicao();

                int Opcao = 0;

                Console.WriteLine(@"
O que deseja alterar?
[1] - Nome
[2] - Idade
[3] - Email
[4] - Não desejo alterar (Sair)");

                while (Opcao < 1 || Opcao > 3)
                {
                    if (Opcao == 4)
                        break;
                    try
                    {
                        Opcao = Convert.ToInt32(Console.ReadLine().Trim());

                        if (Opcao < 1 || Opcao > 4)
                        {
                            Opcao = 0;
                            Console.WriteLine("Opcão inválida. Digite [1], [2] ou [3].");
                        }

                    }
                    catch
                    {
                        Opcao = 0;
                        Console.WriteLine("Opcão inválida. Digite [1], [2] ou [3].");
                        continue;
                    }

                }

                switch (Opcao)
                {
                    case 1:
                        ListaAlunos[Posicao].AlteraNome(CadastrarNomeAluno().ToUpper());
                        Console.WriteLine(@$"Alteração de {ListaAlunos[Posicao].Nome} realizada com sucesso!
{ListaAlunos[Posicao]}");
                        Console.WriteLine();
                        break;
                    case 2:
                        ListaAlunos[Posicao].AlteraIdade(CadastrarIdadeAluno());
                        Console.WriteLine(@$"Alteração de {ListaAlunos[Posicao].Nome} realizada com sucesso!
{ListaAlunos[Posicao]}");
                        break;
                    case 3:
                        ListaAlunos[Posicao].AlteraEmail(CadastrarEmailAluno().ToUpper());
                        Console.WriteLine(@$"Alteração de {ListaAlunos[Posicao].Nome} realizada com sucesso!
{ListaAlunos[Posicao]}");
                        break;
                    case 4:
                        break;

                }

            }

            void RemoverAluno()
            {
                if (!ConfirmaAcao())
                    return;

                Console.Clear();
                Console.WriteLine(@"REMOÇÃO DE ALUNOS +PRATI - GRUPO TELECON
");

                if (!ExisteAlunos())
                {
                    Console.WriteLine("Não há alunos cadastrados para remoção.");
                    return;
                }

                int Posicao = DefinePosicao();

                string NomeAlunoExcluido = ListaAlunos[Posicao].Nome;

                ListaAlunos.RemoveAt(Posicao);


                Console.WriteLine($"Aluno {NomeAlunoExcluido} removido!");
            }

            void ListagemAlunosPosicao()
            {
                Console.Clear();
                Console.WriteLine(@"LISTA DE ALUNOS +PRATI - GRUPO TELECON
");

                if (!ExisteAlunos())
                    Console.WriteLine("Não há alunos cadastrados.");
                else
                {
                    for (byte i = 0; i < ListaAlunos.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {ListaAlunos[i]}");
                    }
                }

            }

            void ListagemAlunosMaiorIdade()
            {
                Console.Clear();
                Console.WriteLine(@"LISTA DE ALUNOS MAIORES DE IDADE +PRATI - GRUPO TELECON
");

                List<Aluno> AlunosMaior = new();

                foreach (Aluno aluno in ListaAlunos)
                {
                    if (aluno.Idade >= 18)
                        AlunosMaior.Add(aluno);
                }

                if (AlunosMaior.Count < 1)
                {
                    Console.WriteLine("Não há alunos maiores de idade cadastrados.");
                    return;
                }


                Console.WriteLine("LISTA ALUNOS MAIORES DE IDADE");

                for (byte i = 0; i < AlunosMaior.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {AlunosMaior[i]}");
                }

            }

            void ListagemAlunosMenorIdade()
            {
                Console.Clear();
                Console.WriteLine(@"LISTA DE ALUNOS MENORES DE IDADE +PRATI - GRUPO TELECON
");

                List<Aluno> AlunosMenor = new();

                foreach (Aluno aluno in ListaAlunos)
                {
                    if (aluno.Idade < 18)
                        AlunosMenor.Add(aluno);

                }

                if (AlunosMenor.Count < 1)
                {
                    Console.WriteLine("Não há alunos menores de idade cadastrados.");
                    return;
                }

                Console.WriteLine(@"LISTA ALUNOS MENORES DE IDADE");

                for (byte i = 0; i < AlunosMenor.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {AlunosMenor[i]}");
                }
            }

            int DefinePosicao()
            {
                int Posicao = -1;

                ListagemAlunosPosicao();

                while (Posicao < 0 || Posicao > ListaAlunos.Count)
                {

                    Console.WriteLine();
                    try
                    {
                        Console.WriteLine($"Informe a posição do aluno desejado");
                        Posicao = Convert.ToInt32(Console.ReadLine().Trim()) - 1;

                        if (Posicao < 0 || Posicao >= ListaAlunos.Count)
                        {
                            Posicao = -1;
                            Console.WriteLine("Posicão inválida. Tecle e tente novamente.");
                            Console.ReadKey();
                        }
                    }
                    catch
                    {
                        Posicao = -1;
                        Console.WriteLine("Posicão inválida. Tecle e tente novamente.");
                        continue;
                    }
                }

                return Posicao;
            }

            bool ExisteAlunos()
            {
                return ListaAlunos.Count > 0;
            }

            int DefineOpcaoMenu()
            {
                int OpcaoEscolhida = -1;

                Console.WriteLine();

                Console.WriteLine("Digite a opção desejada ");

                while (Opcao < 0 || Opcao > 6)
                {

                    try
                    {
                        OpcaoEscolhida = Convert.ToInt32(Console.ReadLine().Trim());

                        if (OpcaoEscolhida < 0 || OpcaoEscolhida > 6)
                        {
                            Console.WriteLine("Opção não é valida. Tente Novamente.");
                            Console.ReadKey();
                        }
                        break;
                    }
                    catch
                    {
                        OpcaoEscolhida = -1;
                        Console.WriteLine("Opção não é valida. Tente Novamente.");
                        Console.ReadKey();
                        continue;
                    }
                }

                return OpcaoEscolhida;
            }

            void ReiniciaTela()
            {
                Opcao = -1;
                Console.WriteLine(@"
Tecle para sair.");
                Console.ReadKey();
                Console.Clear();

            }

            bool ConfirmaAcao()
            {
                Console.WriteLine($"Você realmente deseja executar a ação {DefineAcao(Opcao)}? [1] SIM [2] NÃO");
                int OpcaoConfirma = 0;

                while (OpcaoConfirma < 1 || OpcaoConfirma > 2)
                {
                    try
                    {
                        OpcaoConfirma = Convert.ToInt32(Console.ReadLine().Trim());

                        if (OpcaoConfirma == 1)
                            return true;
                        else if (OpcaoConfirma == 2)
                            return false;
                        else
                        {
                            Console.WriteLine("Opcão inválida. Digite [1] SIM [2] NÃO");
                            OpcaoConfirma = 0;
                            continue;
                        }

                    }
                    catch
                    {
                        Console.WriteLine("Opcão inválida. Digite [1] SIM [2] NÃO");
                        OpcaoConfirma = 0;
                        continue;
                    }
                }

                return false;
            }

            //método auxiliar para ConfirmaAcao()
            string DefineAcao(int Opcao)
            {

                if (Opcao == 1) return "Cadastro de Alunos";
                else if (Opcao == 3) return "Alterar Aluno";
                else return "Remover Aluno";

            }

        }
    }
}
