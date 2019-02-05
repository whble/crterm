using System;
namespace TerminalUI.Terminals
{
    public interface ITerminal
    {
        DisplayControl Display { get; set; }

        /// <summary>
        /// Keyboard map and translation map for this terminal. (ie: Up key sends ^[[A)
        /// </summary>
        TerminalKeyMap KeyMap { get; }

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
        void SendKey(TerminalKeyEventArgs key);
        
        /// <summary>
        /// Handle incoming text, converting escape codes to display actions. 
        /// </summary>
        /// <param name="c"></param>
        void ProcessReceivedCharacter(Char c);

        /// <summary>
        /// Prints the received string on the console. This should interpret control sequences.
        /// </summary>
        /// <param name="v"></param>
        void Print(string v);
        /// <summary>
        /// Print a blank line (or end the current line)
        /// </summary>
        /// <param name="v"></param>
        void PrintLine();
        /// <summary>
        /// Prints the received string on the console with a Newline at the end. This should interpret control sequences.
        /// </summary>
        /// <param name="v"></param>
        void PrintLine(string v);

        /// <summary>
        /// Local edit mode
        /// </summary>
        EchoModes EchoMode { get; set; }

        /// <summary>
        /// Buffer for outgoing text. The terminal does not send text directly. Instead, the frame
        /// buffer raises an event, allowing the host program to read the buffer. 
        /// </summary>
        RingBuffer<char> SendBuffer { get; }
    }

}
