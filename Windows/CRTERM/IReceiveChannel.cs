﻿namespace CRTerm
{
    public interface IReceiveChannel
    {
		/// <summary>
		/// number of bytes waiting to be read
		/// </summary>
		int BytesWaiting { get; }
		/// <summary>
		/// retrieve a byte from the buffer
		/// </summary>
		/// <returns></returns>
		byte Read();
    }
}