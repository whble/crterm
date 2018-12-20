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

        void Clear();
        void Fill(char c);
        void Locate(int Row, int Col);
        void PrintChar(char c);
        void PrintChars(char[] Chars);
        void PrintLine(string s);
        void PrintLineFeed();
        void PrintNewLine();
        void PrintReturn();
        void PrintString(string s);

        event KeyPressEventHandler KeyPressed;

        void ClearTopToCursor();
        void ClearCursorToEnd();
    }
}