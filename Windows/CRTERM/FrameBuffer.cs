using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using TerminalUI.Terminals;

namespace CRTerm
{
    public partial class FrameBuffer : UserControl, IFrameBuffer
    {

        public event TerminalKeyHandler KeyPressed;

        /// <summary>
        /// number of frames to wait to refresh the screen.
        /// One frame = 1/60 second.
        /// </summary>
        private int refreshWait = 0;
        public int BlinkRate = 20;

        //Font Font = SystemFonts.DefaultFont;
        Brush TextBrush = new SolidBrush(Color.LightGreen);
        Brush InvertedBrush = new SolidBrush(Color.Black);
        Brush CursorBrush = new SolidBrush(Color.FromArgb(192, 192, 192));
        float rowHeight = 16;

        static string MEASURE_STRING = new string('W', 80);

        private CursorStyles cursorStyle;
        public CursorStyles CursorStyle
        {
            get
            {
                return this.cursorStyle;
            }

            set
            {
                cursorStyle = value;
            }
        }

        Timer refreshTimer = new Timer();
        /// <summary>
        /// Turns the cursor on and off. Cursor will never be drawn if CursorEnabled=false.
        /// </summary>
        public bool CursorEnabled = true;
        /// <summary>
        /// Blinks the cursor. true when cursor should be drawn on next refresh. False when it should not.
        /// </summary>
        bool CursorState = true;

        /// <summary>
        /// Screen character data. Data is addressed as Data[Row, Col].
        /// </summary>
        public string[,] TextData;
        public StringBuilder CurrentLine = new StringBuilder();
        public ColorCodes[,] ForeColorData;
        public ColorCodes[,] BackColorData;
        public AttributeCodes[,] AttributeData;

        private Terminals.ITerminal _terminal;

        public int PixelsPerRow
        {
            get
            {
                return (int)rowHeight;
            }
        }

        /// <summary>
        /// Column of the cursor position. 0 is left edge
        /// </summary>
        int _cursorCol = 0;
        public int X
        {
            get { return _cursorCol; }
            set
            {
                _cursorCol = value;
                if (_cursorCol < 0)
                    _cursorCol = 0;
                if (_cursorCol >= Cols)
                    _cursorCol = Cols - 1;
                Redraw();
            }
        }

        public FrameBuffer()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrameBuffer_Load);
            CursorStyle = CursorStyles.Underline;
        }

        void FrameBuffer_Load(object sender, EventArgs e)
        {
            //TextFont = GetBestFont();

            this.BackgroundImage = null;
            this.SetBufferSize(25, 80);
            refreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
            refreshTimer.Interval = 1000 / 60;
            this.VisibleChanged += new EventHandler(FrameBufferControl_VisibleChanged);
            this.DoubleBuffered = true;
            this.CursorStyle = CursorStyles.Underline;

            this.Clear();
            if (DesignMode)
            {
                PrintLine("CRTerm: Design mode");
                PrintLine("Area " + this.ClientRectangle.ToString());
                int i;
                for (i = 0; i < Cols; i += 10)
                {
                    string s = (i / 10).ToString();
                    PrintString(s + "         ");
                }
                for (i = 0; i < Cols; i += 1)
                {
                    string s = (i % 10).ToString();
                    PrintString(s);
                }
                i = Y;
                for (i = Y; i < Rows; i++)
                {
                    PrintString(i.ToString());
                    PrintReturn();
                    if (i < Rows - 1)
                        PrintLineFeed();
                }
            }
            else
            {
                if (ParentForm == null)
                    return;
                int htarget = 480;
                PrintLine("CRTerm ©2018 Tom Wilson");
                int topmargin = ParentForm.Height - ClientRectangle.Height;
                int sidemargin = ParentForm.Width - ClientRectangle.Width;
                ParentForm.Height = htarget + topmargin;
                ParentForm.Width = (int)Math.Ceiling(htarget * 1.6) + sidemargin;
            }

        }

        private Font GetBestFont()
        {
            Font useFont = null;
            rowHeight = this.ClientRectangle.Height / (float)Rows;
            if (rowHeight < 8)
                rowHeight = 8;

            var fonts = new[]
            {
                "Classic Console",
                "Glass TTY VT220",
                "Consolas",
                "Lucida Console",
            };

#if DEBUGx
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            var fontFamilies = installedFontCollection.Families;

            // The loop below creates a large string that is a comma-separated
            // list of all font family names.

            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                System.Diagnostics.Debug.WriteLine("Font: " + fontFamilies[j].Name);
            }
#endif

            foreach (var f in fonts)
            {
                using (Font fontTester = new Font(
                        f,
                        rowHeight,
                        FontStyle.Regular,
                        GraphicsUnit.Pixel))
                {
                    if (fontTester.Name == f)
                    {
                        useFont = new Font(f, rowHeight, FontStyle.Regular, GraphicsUnit.Pixel);
                        break;
                    }
                    else
                    {
                    }
                }
            }
            if (useFont == null)
                useFont = new Font(this.Font, FontStyle.Regular);

            Graphics g = this.CreateGraphics();
            SizeF fs = MeasureFont(useFont, g);
            float ratio = rowHeight / fs.Height;
            float newSize = rowHeight * ratio;
            useFont = new Font(useFont.FontFamily, newSize, FontStyle.Regular, GraphicsUnit.Pixel);

            return useFont;
        }

        private void Redraw()
        {
            CursorState = true;
            if (refreshWait > 1)
                refreshWait = 1;
        }

        /// <summary>
        /// Row of cursor position. 0 is top of the screen
        /// </summary>
        int _cursorRow = 0;
        public int Y
        {
            get { return _cursorRow; }
            set
            {
                _cursorRow = value;
                if (_cursorRow < 0)
                    _cursorRow = 0;
                if (_cursorRow >= Rows)
                    _cursorRow = Rows - 1;
                Redraw();
            }
        }

        ColorCodes _currentForeground = ColorCodes.Lightgreen;
        public ColorCodes CurrentForeground
        {
            get { return _currentForeground; }
            protected set { _currentForeground = value; }
        }

        ColorCodes _currentBackground = ColorCodes.Black;
        public ColorCodes CurrentBackground
        {
            get { return _currentBackground; }
            protected set { _currentBackground = value; }
        }

        AttributeCodes _currentAttribute = AttributeCodes.Normal;
        public AttributeCodes CurrentAttribute
        {
            get
            {
                return this._currentAttribute;
            }

            set
            {
                this._currentAttribute = value;
            }
        }

        int _cols = 80;
        public int Cols
        {
            get { return _cols; }
            protected set { _cols = value; }
        }

        int _rows = 25;
        public int Rows
        {
            get { return _rows; }
            protected set { _rows = value; }
        }

        float charWidth = 8;
        float charHeight = 16;
        float xOffset = 0;

        public event DataReadyEventHandler DataReceivedEvent;
        public event StatusChangeEventHandler StatusChangedEvent;

        public ITerminal Terminal
        {
            get
            {
                return _terminal;
            }

            set
            {
                _terminal = value;
            }
        }

        public bool ClearToSend { get; }
        public int BytesWaiting { get; }
        public ConnectionStatusCodes Status { get; }
        public string StatusDetails { get; }

        public virtual void SetBufferSize(int Rows, int Cols)
        {
            this._cols = Cols;
            this._rows = Rows;
            TextData = new string[Rows, Cols];
            ForeColorData = new ColorCodes[Rows, Cols];
            BackColorData = new ColorCodes[Rows, Cols];
            AttributeData = new AttributeCodes[Rows, Cols];
            Font = GetBestFont();
        }

        public virtual void SetCell(int Row, int Col, char c, ColorCodes ForeColor, ColorCodes BackColor, AttributeCodes Attribute)
        {
            if (Row < 0 || Row >= Rows || Col < 0 || Col >= Cols)
                return;
            TextData[Row, Col] = c.ToString();
            ForeColorData[Row, Col] = ForeColor;
            BackColorData[Row, Col] = BackColor;
            AttributeData[Row, Col] = Attribute;
        }

        public virtual void SetCell(int Row, int Col, char c)
        {
            SetCell(Row, Col, c, CurrentForeground, CurrentBackground, CurrentAttribute);
        }

        public virtual void SetCell(char c)
        {
            //SetCell(X, Y, c, CurrentForeground, CurrentBackground, CurrentAttribute);
            SetCell(Y, X, c, CurrentForeground, CurrentBackground, CurrentAttribute);
        }

        public virtual void PrintChar(char c)
        {
            SetCell(c);
            AdvanceCursor();
            Redraw();
        }

        public virtual void PrintText(char[] c)
        {
            for (int i = 0; i < c.Length; i++)
                PrintChar(c[i]);
        }

        private void Scroll1()
        {
            for (int row = 0; row < Rows - 1; row++)
            {
                Array.Copy(TextData, (row + 1) * Cols, TextData, row * Cols, Cols);
                Array.Copy(ForeColorData, (row + 1) * Cols, ForeColorData, row * Cols, Cols);
                Array.Copy(BackColorData, (row + 1) * Cols, BackColorData, row * Cols, Cols);
                Array.Copy(AttributeData, (row + 1) * Cols, AttributeData, row * Cols, Cols);
                //for (int col = 0; col < Cols; col++)
                //{
                //    CharacterData[row, col] = CharacterData[row + 1, col];
                //    ForeColorData[row, col] = ForeColorData[row + 1, col];
                //    BackColorData[row, col] = BackColorData[row + 1, col];
                //    AttributeData[row,col] = AttributeData[row + 1, col];
                //}
            }

            for (int col = 0; col < Cols; col++)
            {
                TextData[Rows - 1, col] = " ";
                ForeColorData[Rows - 1, col] = CurrentForeground;
                BackColorData[Rows - 1, col] = CurrentBackground;
                AttributeData[Rows - 1, col] = CurrentAttribute;
            }

            // force redraw on next refresh interval
            Redraw();
        }

        public void AdvanceCursor()
        {
            CursorState = true;
            if (X < Cols - 1)
                X += 1;
            else
            {
                PrintReturn();
                PrintLineFeed();
            }
        }

        public void PrintLineFeed()
        {
            if (Y < Rows - 1)
                Y += 1;
            else
            {
                Scroll1();
                Y = Rows - 1;
            }
        }

        public void PrintReturn()
        {
            X = 0;
        }

        public void PrintNewLine()
        {
            PrintReturn();
            PrintLineFeed();
        }

        public virtual void PrintLine(string s)
        {
            PrintString(s);
            PrintReturn();
            PrintLineFeed();
        }

        public virtual void PrintString(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                PrintChar(s[i]);
            }
        }

        /// <summary>
        /// Moves the cursor on the screen. This is zero-based
        /// </summary>
        /// <param name="Row">Row number, top of screen is 0</param>
        /// <param name="Col">Column, left side of screen is 0</param>
        public virtual void Locate(int Row, int Col)
        {
            Y = Row;
            X = Col;

            if (Row < 0)
                Row = 0;
            if (Row >= Rows)
                Row = Rows - 1;
            if (Col < 0)
                Col = 0;
            if (Col >= Cols)
                Col = Cols - 1;

            CursorState = true;
        }

        public virtual void Clear()
        {
            Fill(' ');
            Locate(0, 0);
            Redraw();
        }

        public virtual void Fill(char c)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    SetCell(row, col, c);
                }
            }
        }

        void CreateImage()
        {
            if (Font == null)
                Font = GetBestFont();
            this.BackgroundImage = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);

            Graphics g = Graphics.FromImage(this.BackgroundImage);
            SizeF charSize = MeasureFont(Font, g);

            charWidth = charSize.Width / MEASURE_STRING.Length;
            charHeight = charSize.Height;

            int w = (int)(charWidth * this.Cols);
            int h = (int)(charHeight * this.Rows);
            this.BackgroundImage = new Bitmap(w, h);
        }

        void DrawText()
        {
            if (this.BackgroundImage == null)
                CreateImage();

            Graphics g = Graphics.FromImage(this.BackgroundImage);
            float x;
            float y;

            float RightCol = charWidth * this.Cols;

            //float scaleFactor = this.ClientRectangle.Width / Col80;
            //g.ScaleTransform(scaleFactor, scaleFactor);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(Color.Black);
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    x = col * charWidth;
                    y = row * charHeight;
                    g.DrawString(TextData[row, col].ToString(), Font, TextBrush, x, y, StringFormat.GenericTypographic);
                }
            }
            if (CursorEnabled && CursorState)
            {
                x = X * charWidth;
                y = Y * charHeight;
                float h = charHeight / 4;
                switch (CursorStyle)
                {
                    case CursorStyles.None:
                        break;
                    case CursorStyles.Underline:
                        g.FillRectangle(CursorBrush, x, y + charHeight - h, charWidth, h);
                        break;
                    case CursorStyles.Block:
                        g.FillRectangle(CursorBrush, x, y, charWidth, charHeight);
                        break;
                    case CursorStyles.Insert:
                        g.FillRectangle(CursorBrush, x, y, charWidth / 4, charHeight);
                        break;
                    default:
                        break;
                }
            }
            this.Invalidate();
        }

        private SizeF MeasureFont(Font font, Graphics g)
        {
            return g.MeasureString(MEASURE_STRING, font, int.MaxValue, StringFormat.GenericTypographic);
        }

        void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (refreshWait-- > 0)
                return;

            DrawText();
            CursorState = !CursorState;
            refreshWait = BlinkRate;
        }

        void FrameBufferControl_VisibleChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = this.Visible;
        }

        private void FrameBuffer_SizeChanged(object sender, System.EventArgs e)
        {
            BackgroundImage = null;
            Font = GetBestFont();
            Redraw();
        }

        private void FrameBuffer_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("key code:" + e.KeyCode.ToString());
            //TerminalKeyEventArgs args = new TerminalKeyEventArgs(e.KeyChar);
        }

        private void FrameBuffer_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void FrameBuffer_KeyPress(object sender, KeyPressEventArgs e)
        {
            TerminalKeyEventArgs args = new TerminalKeyEventArgs(e.KeyChar);
            KeyPressed?.Invoke(this, args);
        }

        public byte ReadByte()
        {
            return 0;
        }

        public void ClearTopToCursor()
        {
            for (int r = 0; r < Y - 1; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    SetCell(r, c, ' ');
                }
            }
            for (int c = 0; c < X; c++)
            {
                SetCell(Y, c, ' ');
            }
        }

        public void ClearCursorToEnd()
        {
            for (int c = X; c < Cols; c++)
            {
                SetCell(Y, c, ' ');
            }
            for (int r = Y + 1; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    SetCell(r, c, ' ');
                }
            }
        }

    }
}
