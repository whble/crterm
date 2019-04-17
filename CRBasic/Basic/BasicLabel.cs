using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class BasicLabel
    {
        /// <summary>
        /// Name of the label (without :). 
        /// </summary>
        public string Name;
        /// <summary>
        /// For line numbered BASIC, populate this with the entered line number. Set to -1 for immediate mode
        /// and source code without line numbers.
        /// </summary>
        public int LineNumber = -1;
        /// <summary>
        /// A use of this label has been found (ie: GOTO or GOSUB). Line number labels (_ln0000) will be removed 
        /// before compilation if they are not referenced.
        /// </summary>
        public bool FoundReference;
        /// <summary>
        /// Set to True when the label declaration is processed. If two different instances of a label are found,
        /// or if the label isn't formally declared, this will create an error during translation. 
        /// </summary>
        public bool FoundDeclaration;
    }
}
