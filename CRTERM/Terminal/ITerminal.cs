using System;
using CRTERM.Common;

namespace CRTERM.Terminal
{
	/// <summary>
	/// Provides an emulation layer. The emulation layer converts physical keypresses to 
	/// byte sequences and interprets byte seuqeunces in order to display text on the screen.
	/// For example, the Escape character followed by [A would move the cursor up, and
	/// the byte value 64 will display the @ symbol on the screen.
	/// <para>An Terminal should talk to the modem and manipuate the frame buffer.</para>
	/// <para>The modem should talk to the transport</para>
	/// <para>The Transport carries data to the remote system</para>
	/// </summary>
	public interface ITerminal : ICommProvider
	{
		/// <summary>
		/// Handle a key by its scancode. This is only used for non-printable keys (F-keys, arrows, etc.)
		/// </summary>
		/// <param name="e"></param>
		void KeyDown(System.Windows.Forms.KeyEventArgs e);
		/// <summary>
		/// Handle all printable keys
		/// </summary>
		/// <param name="e"></param>
		void KeyPress(System.Windows.Forms.KeyPressEventArgs e);
		CRTERM.Modem.IModem Modem { get; set; }

		event TextReceivedEventHandler TextReceived;
		event TerminalEventHandler TerminalEvent;
		event CursorEventHandler CursorEvent;
		event ColorEventHandler ColorEvent;
		event SimpleEventHandler EnterKeyReceived;

		void Write(string Text);
		void WriteLine(string Text);
		void SetColor(ConsoleColor Foreground, ConsoleColor Background);
		void CursorAction(CursorCommandCodes Command, int Row, int Col);
		void SendBreak();
	}
}
