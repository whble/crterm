using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm
{
    public enum ConnectionStatusCodes
    {
        Disconnected = 0,
        Opening,
        Connected,
        Disconnecting,
        UnexpectedDisconnect,
        Retrying,
    }
}
