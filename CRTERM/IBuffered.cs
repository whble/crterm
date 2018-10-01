using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm
{
    public interface IBuffered
    {

        /// <summary>
        /// number of bytes waiting to be read
        /// </summary>
        int BytesWaiting { get; }
        /// <summary>
        /// retrieve a byte from the buffer
        /// </summary>
        /// <returns></returns>
        byte ReadByte();
    }
}
