using TerminalControl.Terminals;

namespace TerminalControl
{
    public interface IFrameBuffer
    {
        int Cols { get; }
        ColorCodes CurrentBackground { get; }
        ColorCodes CurrentForeground { get; }
        int Rows { get; }
        int X { get; set; }
        int Y { get; set; }
        CursorStyles CursorStyle { get; set; }

        void Clear();
        void Fill(char c);
        void Locate(int Row, int Col);
        void PrintChar(char c);
        void Print(char[] c);
        void PrintLine(string s);
        void PrintLineFeed();
        void PrintNewLine();
        void PrintReturn();
        void Print(string s);

        EditModes EditMode { get; set; }
        bool AddLinefeed { get; set; }
        bool BackspaceDelete { get; set; }
        bool BackspaceOverwrite { get; set; }
        bool BackspacePull { get; set; }
        bool LineWrap { get; set; } 

        event TerminalKeyHandler KeyPressed;

        void ClearTopToCursor();
        void ClearCursorToEnd();
    }
}