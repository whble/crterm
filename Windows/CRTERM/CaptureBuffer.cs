using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalUI
{
    public class CaptureBuffer
    {
        public enum CaptureStatusCodes
        {
            Closed,
            Capturing,
            Saved
        }

        public CaptureStatusCodes Status = CaptureStatusCodes.Closed;
        public StringBuilder Buffer = new StringBuilder();
        public string StatusText
        {
            get
            {
                if (Status == CaptureStatusCodes.Closed)
                    return "";

                if (Status == CaptureStatusCodes.Capturing)
                    return Status.ToString() + " " + Buffer.Length.ToString();

                return Status.ToString();
            }
        }

        public override string ToString()
        {
            return Buffer.ToString();
        }


    }
}
