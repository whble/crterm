using CRTerm.Terminals;

namespace CRTerm
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
        void PrintText(char[] c);
        void PrintLine(string s);
        void PrintLineFeed();
        void PrintNewLine();
        void PrintReturn();
        void PrintString(string s);

        event TerminalKeyHandler KeyPressed;

        void ClearTopToCursor();
        void ClearCursorToEnd();
    }
}