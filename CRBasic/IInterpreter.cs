using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic
{
    public interface IInterpreter
    {
        void Init();
        TerminalUI.DisplayControl Display { get; set; }
        List<String> ProgramText { get; }
        void Execute(string Line);
        void Run();
    }
}
