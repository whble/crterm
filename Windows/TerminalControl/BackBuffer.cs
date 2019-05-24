using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalUI
{
    public class BackBuffer
    {
        /// <summary>
        /// never keep more than this many lines
        /// </summary>
        public int CapacityMax = 70000;
        /// <summary>
        /// Never keep fewer than this many lines, once the buffer has filled.
        /// </summary>
        public int CapacityMin = 66000;

        public List<string> Lines = new List<string>();

        public void Add(string Line)
        {
            Lines.Add(Line);
            if (Lines.Count > 70000)
                Lines.RemoveRange(0, CapacityMax - CapacityMin);
        }

        public string this[int i]
        {
            get
            {
                return Lines[i];
            }
        }
    }
}
