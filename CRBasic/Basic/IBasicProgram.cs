using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{

    public interface IBasicProgram
    {
        BasicVariables Globals { get; set; }

        void Run();
        void Break();
        void End();
    }
}
