using System;
namespace CRTerm.Terminals
{
    public interface ITerminal : IConfigurable, IHasStatus, IBuffered
    {
        event DataReadyEventHandler DataSent;
        IFrameBuffer FrameBuffer { get; set; }
        bool BasicMode { get; set; }

        /// <summary>
        /// Sends a character from the keyboard. This should be converted to ASCII and sent straight through.
        /// </summary>
        /// <param name="c"></param>
        void SendChar(char c);
        /// <summary>
        /// Send a sequence of characters. Text should be sent as-is. 
        /// Control characters should be stripped. 
        /// CR, LF, or CRLF should be converted to CR. 
        /// </summary>
        /// <param name="Text"></param>
        void SendString(string Text);
        /// <summary>
        /// Sends the terminal sequence for a keyboard command. 
        /// F-keys and arrow keys should be translated to escape sequences or control codes. 
        /// </summary>
        /// <param name="KeyArgs"></param>
        void SendKey(TerminalKeyEventArgs KeyArgs);
        /// <summary>
        /// Handle incoming text, converting escape codes to display actions. 
        /// </summary>
        /// <param name="c"></param>
        void ProcessReceivedCharacter(Char c);
        void Print(string v);
        void ReceiveData(IBuffered channel);
    }

}
