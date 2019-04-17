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
        /// <summary>
        /// Execute a single statement, usually in immediate mode. This should also allow entry of text in immediate mode, ie 
        /// by prefixing a statement with a line number or using the AUTO statement. 
        /// </summary>
        /// <param name="Line"></param>
        void ExecuteStatement(string Line);
        /// <summary>
        /// Start the program
        /// </summary>
        /// <param name="EntryPoint">Method, function, or label to jump to. Implementation dependent.</param>
        void Run(string EntryPoint="");
        /// <summary>
        /// Load a file from disk.
        /// </summary>
        /// <param name="Filename"></param>
        void Load(string Filename);
    }
}
