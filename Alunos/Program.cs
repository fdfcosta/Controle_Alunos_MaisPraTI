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
            ControleAlunos c = new();

            c.ImprimeMenu();
        }
    }
}
