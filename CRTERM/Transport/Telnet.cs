using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* 
 * The Network Virtual Terminal is a Telnet protocol layer that negotiates terminal
 * capabilities. These may include things like text/binary transfer, "Go Ahead", and 
 * Terminal Type. To keep things simple, we will only respond to "Suppress Go Ahead".
 */

namespace CRTERM.Transport
{
	public class Telnet : TCP
	{
		public Telnet()
		{
		}

		private States ReadState = States.ReadingData;
		enum States
		{
			ReadingData,
			ReadingVerb,
			ReadingOption
		}

		public enum ControlChars
		{
			NUL = 0,
			BEL = 7,
			BS = 8,
			HT = 9,
			LF = 10,
			VT = 11,
			FF = 12,
			CR = 13,
		}

		public const byte IAC = 255;
		public enum Verbs : byte
		{
			SE = 240,
			NOP = 241,
			DM = 242,
			BRK = 243,
			IP = 244,
			AO = 245,
			AYT = 246,
			EC = 247,
			EL = 248,
			GA = 249,
			SB = 250,
			WILL = 251,
			WONT = 252,
			DO = 253,
			DONT = 254,
			IAC = 255
		};

		public enum Options : byte
		{
			BIN = 0,     // Binary Transmission
			ECHO = 1,    // Echo
			RECN = 2,    // Reconnection
			SGA = 3,     // Suppress Go Ahead
			APRX = 4,    // Approx Message Size Negotiation
			STAT = 5,    // Status
			TIM = 6,     // Timing Mark
			REM = 7,     // Remote Controlled Trans and Echo
			OLW = 8,     // Output Line Width
			OPS = 9,     // Output Page Size
			OCRD = 10,   // Output Carriage-Return Disposition
			OHT = 11,    // Output Horizontal Tabstops
			OHTD = 12,   // Output Horizontal Tab Disposition
			OFD = 13,    // Output Formfeed Disposition
			OVT = 14,    // Output Vertical Tabstops
			OVTD = 15,   // Output Vertical Tab Disposition
			OLD = 16,    // Output Linefeed Disposition
			EXT = 17,    // Extended ASCII
			LOGO = 18,   // Logout
			BYTE = 19,   // Byte Macro
			DATA = 20,   // Data Entry Terminal
			SUP = 21,    // SUPDUP
			SUPO = 22,   // SUPDUP Output
			SNDL = 23,   // Send Location
			TERM = 24,   // Terminal Type
			EOR = 25,    // End of Record
			TACACS = 26, // TACACS User Identification
			OM = 27,     // Output Marking
			TLN = 28,    // Terminal Location Number
			TN3270 = 29, // Telnet 3270 Regime
			X3 = 30,     // X.3 PAD
			NAWS = 31,   // Negotiate About Window Size
			TS = 32,     // Terminal Speed
			RFC = 33,    // Remote Flow Control
			LINE = 34,   // Linemode
			XDL = 35,    // X Display Location
			ENVIR = 36,  // Telnet Environment Option
			AUTH = 37,   // Telnet Authentication Option
			NENVIR = 39, // Telnet Environment Option
			EXTOP = 255  // Extended-Options-List
		};

		bool SGA = false;
		Verbs LastVerbRec;

		Options LastOptionSent = Options.SGA;
		Verbs LastVerbSent = Verbs.NOP;

		/// <summary>
		/// Request that the remote system Do or Don't use an option. 
		/// <example>DoOption(Option.Echo,True);
		///		This will request that the remote system echo received text.
		/// </example>
		/// </summary>
		/// <param name="Option"></param>
		/// <param name="YesNo">True=DO, False=DONT</param>
		public void SendOption(Verbs Verb, Options Option)
		{
			if (Option == LastOptionSent && Verb == LastVerbSent)
				return;

			System.Diagnostics.Debug.WriteLine("SEND " + Verb.ToString() + " " + Option.ToString());
			byte[] d = new byte[3];
			d[0] = IAC;
			d[1] = (byte)Verb;
			d[2] = (byte)Option;

			Send(d);
			if (SGA)
				Send(new byte[] { (byte)Verbs.GA });

			LastOptionSent = Option;
			LastVerbSent = Verb;
		}

		virtual protected void HandleNVTCmd(Verbs Verb, Options Option)
		{
			System.Diagnostics.Debug.WriteLine("RECV " + Verb.ToString() + " " + Option.ToString());
			switch (Verb)
			{
				case Verbs.SE:
					break;
				case Verbs.NOP:
					break;
				case Verbs.DM:
					break;
				case Verbs.BRK:
					break;
				case Verbs.IP:
					break;
				case Verbs.AO:
					break;
				case Verbs.AYT:
					break;
				case Verbs.EC:
					break;
				case Verbs.EL:
					break;
				case Verbs.GA:
					break;
				case Verbs.SB:
					break;
				case Verbs.WILL:
					if (Option == Options.ECHO || Option == Options.BIN || Option == Options.SGA)
						SendOption(Verbs.DO, Option);
					else
						SendOption(Verbs.DONT, Option);
					break;
				case Verbs.WONT:
					break;
				case Verbs.DO:
					if (Option == Options.SGA)
					{
						SGA = true;
						SendOption(Verbs.WILL, Options.SGA);
					}
					else if (Option == Options.BIN)
					{
						SendOption(Verbs.WILL, Option);
					}
					else
						SendOption(Verbs.WONT, Option);
					break;
				case Verbs.DONT:
					if (Option == Options.SGA)
					{
						SGA = false;
						SendOption(Verbs.WONT, Options.SGA);
					}
					else
						SendOption(Verbs.WONT, Option);
					break;
			}
			ReadState = States.ReadingData;
		}

		public override void Parse(byte b)
		{
			switch (ReadState)
			{
				case States.ReadingData:
					if (b == IAC)
						ReadState = States.ReadingVerb;
					else
					{
						ReceiveBuffer.Write(b);
						if (b < 32 || b > 127)
							System.Diagnostics.Debug.Write("[" + (int)b + "]");
					}
					break;
				case States.ReadingVerb:
					LastVerbRec = (Verbs)b;
					ReadState = States.ReadingOption;
					break;
				case States.ReadingOption:
					HandleNVTCmd(LastVerbRec, (Options)b);
					break;
			}
		}

		public void SendOptions()
		{
			SendOption(Verbs.WILL, Options.SGA);
		}

		protected override void Connect(string HostName, int PortNumber)
		{
			base.Connect(HostName, PortNumber);
			//SendOption(Verbs.WILL, Options.SGA);
		}
	}
}
