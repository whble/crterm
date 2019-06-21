using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TerminalUI.Terminals;

namespace TerminalUI
{
    public partial class DisplayControl : UserControl
    {
        #region Fields

        private static string MEASURE_STRING = new string('W', 80);
        public TextDialog CurrentDialog = null;

        private int _cols;
        private int _rows;
        private float RowHeight = 16;
        private float ColWidth = 8;
        private int x;
        private int y;

        private ScreenBuffer SaveBuffer = null;

        private Pen borderPen = new Pen(Color.FromArgb(32, 32, 32));

        public List<KeyEventArgs> Hotkeys = new List<KeyEventArgs>();

        public TextCursorStyles TextCursor { get; set; }

        private InsertKeyMode _insertMode = InsertKeyMode.Overwrite;

        private EchoModes _echoMode;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool AddLinefeed { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool LineWrap { get; set; }

        private string _statusText;

        internal void ClearRectangle(int x, int y, int width, int height)
        {
            for (int row = y; row < y + height; row++)
            {
                for (int col = x; col < x + width; col++)
                {
                    SetCharacter(row, col, ' ');
                }
            }
        }

        internal void DrawRectangle(int left, int top, int width, int height)
        {
            SetCharacter(top, left, '╔');
            SetCharacter(top, left + width - 1, '╗');
            SetCharacter(top + height - 1, left, '╚');
            SetCharacter(top + height - 1, left + width - 1, '╝');

            for (int col = left + 1; col < left + width - 1; col++)
            {
                SetCharacter(top, col, '═');
                SetCharacter(top + height - 1, col, '═');
            }
            for (int row = top + 1; row < top + height - 1; row++)
            {
                SetCharacter(row, left, '║');
                SetCharacter(row, left + width - 1, '║');
            }
        }

        protected bool FontValid = false;
        private Timer drawTimer = new Timer();

        public bool CursorEnabled;
        public bool CursorOn;
        public int NextDraw = 0;
        public int BlinkInterval = 20;

        public CharacterCell.ColorCodes CurrentBackground { get; set; }

        public CharacterCell.ColorCodes CurrentTextColor { get; set; }

        public CharacterCell.AttributeCodes CurrentAttribute { get; set; }

        private IEditorPlugin _editor = null;
        private ITerminal _terminal = new Terminals.ANSITerminal();

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public List<string> Buffer = new List<string>();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]

        public char[] CharacterData = new char[2000];
        public CharacterCell.ColorCodes[] TextColorData = new CharacterCell.ColorCodes[2000];
        public CharacterCell.ColorCodes[] BackColorData = new CharacterCell.ColorCodes[2000];
        public CharacterCell.AttributeCodes[] AttributeData = new CharacterCell.AttributeCodes[2000];


        #endregion

        #region Events
        public event EventHandler ToggleFullScreenRequest;
        public event KeyEventHandler HotkeyPressed;
        #endregion

        #region Public Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public EchoModes EchoMode
        {
            get { return _echoMode; }
            set
            {
                _echoMode = value;
                if (Terminal != null)
                {
                    Terminal.EchoMode = value;
                }
            }
        }

        public void PrintSeparater()
        {
            if (CurrentColumn > 0)
                PrintLine();
            PrintLine(new string('─', 72));
        }

        public ITerminal Terminal
        {
            get
            {
                return _terminal;
            }

            set
            {
                _terminal = value;
                if (_terminal != null)
                {
                    _terminal.Display = this;
                }
            }
        }

        public int CurrentColumn
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
                if (x < 0)
                {
                    x = 0;
                }

                if (x >= Columns)
                {
                    int r = (int)(x / Columns);
                    CurrentRow += r;
                    x = x % Columns;
                }

                BlinkCursor();
            }
        }

        public char CharUnderCursor
        {
            get
            {
                return CharacterData[CursorPos];
            }
            set
            {
                SetCharacter(value);
            }
        }

        public InsertKeyMode InsertMode
        {
            get
            {
                return this._insertMode;
            }

            set
            {
                this._insertMode = value;
                UpdateTextCursorMode();
            }
        }

        public string StatusText
        {
            get
            {
                return this._statusText;
            }

            set
            {
                this._statusText = value;
            }
        }

        public string ModeText
        {
            get
            {
                return "Text " + Columns.ToString() + "x" + Rows.ToString();
            }
        }

        #endregion

        #region Methods

        public void MoveToSOL()
        {
            CurrentColumn = 0;
        }


        //
        // Insert a blank character at the cursor. Character in right column will be lost off right side of the screen.
        //
        private void Insert()
        {
            for (int col = Columns - 1; col > CurrentColumn; col--)
            {
                int pos = GetPos(CurrentRow, col);
                CopyCharacter(pos, pos - 1);
            }
            SetCharacter(CurrentRow, CurrentColumn, ' ');
            BlinkCursor();
        }

        private void InsertLine(int Row)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            for (int col = CurrentColumn + 1; col < Columns - 1; col++)
            {
                int pos = GetPos(CurrentRow, col);
                CopyCharacter(pos - 1, pos);
            }
            SetCharacter(CurrentRow, Columns - 1, ' ');
            BlinkCursor();
        }

        public int CurrentRow
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
                if (y < 0)
                {
                    y = 0;
                }

                if (y >= Rows)
                {
                    y = Rows - 1;
                }

                BlinkCursor();
            }
        }

        public int Columns
        {
            get
            {
                return _cols;
            }
        }

        public int Rows
        {
            get
            {
                return _rows;
            }
        }

        public int CursorPos
        {
            get
            {
                return CurrentRow * Columns + CurrentColumn;
            }
            set
            {
                CurrentRow = value / Columns;
                CurrentColumn = value % Columns;
            }
        }

        public int CurrentRowStart
        {
            get
            {
                return CurrentRow * Columns;
            }
        }

        public IEditorPlugin Editor
        {
            get
            {
                return _editor;
            }

            set
            {
                _editor = value;
                if (_editor != null)
                {
                    _editor.Display = this;
                    _editor.Terminal = Terminal;
                }
            }
        }

        internal void ClearScreen(bool FromStart, bool ToEnd)
        {
            int start = FromStart ? 0 : CursorPos;
            int end = ToEnd ? CharacterData.Length - 1 : CursorPos;
            for (int i = start; i <= end; i++)
            {
                SetCharacter(i, ' ', CurrentTextColor, CurrentBackground, CurrentAttribute);
            }
        }

        public int GetPos(int row, int col)
        {
            int pos = row * Columns + col;
            if (pos < 0)
                return 0;
            if (pos >= CharacterData.Length)
                return CharacterData.Length - 1;
            return pos;
        }


        public static Brush[] Brushes = new SolidBrush[] {
            new SolidBrush(Color.FromArgb(0, 0, 0)),
            new SolidBrush(Color.FromArgb(0, 0, 128)),
            new SolidBrush(Color.FromArgb(0, 128, 0)),
            new SolidBrush(Color.FromArgb(0, 128, 128)),
            new SolidBrush(Color.FromArgb(128, 0, 0)),
            new SolidBrush(Color.FromArgb(0, 0, 128)),
            new SolidBrush(Color.FromArgb(0, 128, 128)),
            new SolidBrush(Color.FromArgb(128, 128, 128)),
            new SolidBrush(Color.FromArgb(64, 64, 64)),
            new SolidBrush(Color.FromArgb(0, 0, 255)),
            new SolidBrush(Color.FromArgb(0, 255, 0)),
            new SolidBrush(Color.FromArgb(0, 255, 255)),
            new SolidBrush(Color.FromArgb(255, 0, 0)),
            new SolidBrush(Color.FromArgb(0, 0, 255)),
            new SolidBrush(Color.FromArgb(0, 255, 255)),
            new SolidBrush(Color.FromArgb(255, 255, 255)),
        };

        public DisplayControl()
        {
            InitializeComponent();

            CurrentTextColor = CharacterCell.ColorCodes.Gray;
            SetTextMode(25, 80);
            DoubleBuffered = true;
            DoubleBuffered = true;
            LineWrap = true;

            drawTimer.Interval = 1000 / 60;
            drawTimer.Tick += DrawTimer_Tick;

            if (!DesignMode)
                drawTimer.Enabled = true;

            Resize += TerminalControl_Resize;
            KeyDown += HandleKeyDown;
            KeyPress += HandleKeyPress;
            KeyUp += HandleKeyUp;
        }

        public void TerminalControl_Resize(object sender, EventArgs e)
        {
            FontValid = false;
            NextDraw = 0;
        }

        public void SetTextMode(int Rows, int Columns)
        {
            if (Rows <= 0 || Columns <= 0)
            {
                throw new ArgumentException("Rows and Columns must be >0. Got Rows=" + Rows.ToString() + ", Columns=" + Columns.ToString());
            }

            _rows = Rows;
            _cols = Columns;
            int len = Rows * Columns;

            CharacterData = new char[len];
            TextColorData = new CharacterCell.ColorCodes[len];
            BackColorData = new CharacterCell.ColorCodes[len];
            AttributeData = new CharacterCell.AttributeCodes[len];
            Clear();
        }

        /// <summary>
        /// Clears the current screen line. To clear the entire line,
        /// set both parametrs to True. To clear the start of the line up to the cursor,
        /// set FromStart true. To clear from the cursor to the end, set ToEnd to true.
        /// </summary>
        /// <param name="FromStart"></param>
        /// <param name="ToEnd"></param>
        public void ClearCurrentLine(bool FromStart, bool ToEnd)
        {
            int start = FromStart ? 0 : CurrentColumn;
            int end = ToEnd ? Columns : CurrentColumn + 1;
            for (int col = start; col < end; col++)
            {
                SetCharacter(CurrentRow, col, ' ');
            }
            BlinkCursor();
        }

        public void Fill(char c)
        {
            for (int i = 0; i < CharacterData.Length; i++)
            {
                SetCharacter(i, c, CurrentTextColor, CurrentBackground, CharacterCell.AttributeCodes.Normal);
            }
        }

        public void HandleKeyDown(object sender, KeyEventArgs e)
        {
            foreach (KeyEventArgs key in Hotkeys)
            {
                if (key.Modifiers == e.Modifiers && key.KeyCode == e.KeyCode)
                {
                    HotkeyPressed?.Invoke(sender, e);
                }
            }

            if (!e.Handled)
            {

                switch (e.KeyCode)
                {
                    case Keys.F11:
                        if (ToggleFullScreenRequest != null)
                        {
                            ToggleFullScreenRequest(sender, e);
                            e.Handled = true;
                        }
                        break;
                }
            }

            if (!e.Handled)
            {
                switch (EchoMode)
                {
                    case EchoModes.LineEdit:
                        break;
                    case EchoModes.FullScreenEdit:
                        e.Handled = HandleEditKey(e);
                        break;
                    case EchoModes.Plugin:
                        e.Handled = true;
                        Editor?.HandleKeyDown(sender, e);
                        break;
                    case EchoModes.EchoOff:
                    case EchoModes.LocalEcho:
                    default:
                        if (e.KeyCode == Keys.F12)
                        {
                            EchoMode = EchoModes.FullScreenEdit;
                            InsertMode = InsertKeyMode.Overwrite;
                            BlinkCursor();
                            e.Handled = true;
                        }
                        break;
                }
            }

            if (!e.Handled)
            {
                TerminalKeyEventArgs k = new TerminalKeyEventArgs(e);
                Terminal.SendKey(k);
            }
        }

        private bool HandleEditKey(KeyEventArgs e)
        {
            bool handled = true;
            switch (e.KeyCode)
            {
                case Keys.F12:
                    EchoMode = EchoModes.EchoOff;
                    CurrentColumn = 0;
                    int y = Rows - 1;
                    while (y>0 && GetChar(y - 1, 0) == ' ')
                        y = y - 1;
                    CurrentRow = y;
                    BlinkCursor();
                    break;
                case Keys.Return:
                    StringBuilder s = new StringBuilder();
                    for (int col = 0; col < Columns; col++)
                        s.Append(GetChar(CurrentRow, col));
                    string ss = s.ToString().Trim();
                    CurrentColumn = 0;
                    ClearCurrentLine(true, true);
                    Terminal?.SendString(ss);
                    Terminal?.SendChar('\r');
                    InsertMode = InsertKeyMode.Overwrite;
                    if (ss.Trim().ToLower() == "run")
                        EchoMode = EchoModes.EchoOff;
                    break;
                case Keys.Up:
                    CurrentRow -= 1;
                    break;
                case Keys.Down:
                    CurrentRow += 1;
                    break;
                case Keys.Right:
                    CurrentColumn += 1;
                    break;
                case Keys.Left:
                    CurrentColumn -= 1;
                    break;
                case Keys.Back:
                    CurrentColumn -= 1;
                    Delete();
                    break;
                case Keys.Delete:
                    Delete();
                    break;
                case Keys.Insert:
                    if (InsertMode == InsertKeyMode.Insert)
                        InsertMode = InsertKeyMode.Overwrite;
                    else
                        InsertMode = InsertKeyMode.Insert;
                    BlinkCursor();
                    break;
                case Keys.Home:
                    if (e.Control)
                        Clear();
                    CurrentColumn = 0;
                    break;
                case Keys.End:
                    if (e.Control)
                        ClearCurrentLine(false, true);
                    else
                    {
                        CurrentColumn = Columns - 1;
                        while (CurrentColumn > 0 && CharacterData[CursorPos - 1] == ' ')
                            CurrentColumn -= 1;
                    }
                    break;
                default:
                    handled = false;
                    break;
            }
            return handled;
        }

        private char GetChar(int Row, int Col)
        {
            int pos = GetPos(Row, Col);
            return CharacterData[pos];
        }

        public void HandleKeyPress(object sender, KeyPressEventArgs e)
        {
            bool handled = true;
            switch (EchoMode)
            {
                case EchoModes.LocalEcho:
                    handled = false;
                    Terminal.ProcessReceivedCharacter(e.KeyChar);
                    break;
                case EchoModes.LineEdit:
                    break;
                case EchoModes.FullScreenEdit:
                    // break key
                    // turn off BASIC mode and pass the key through
                    if (e.KeyChar == 3)
                    {
                        EchoMode = EchoModes.EchoOff;
                        InsertMode = InsertKeyMode.Overwrite;
                        handled = false;
                    }
                    if (e.KeyChar >= ' ')
                    {
                        if (InsertMode == InsertKeyMode.Insert)
                            Insert();
                        Print(e.KeyChar);
                    }
                    break;
                case EchoModes.Plugin:
                    handled = true;
                    Editor?.HandleKeyPress(sender, e);
                    break;
                case EchoModes.EchoOff:
                default:
                    handled = false;
                    break;
            }

            if (!handled)
            {
                Terminal.SendChar(e.KeyChar);
            }
        }

        public void HandleKeyUp(object sender, KeyEventArgs e)
        {
        }

        public void Locate(int Row, int Col)
        {
            CurrentRow = Row;
            CurrentColumn = Col;
        }

        public void Print(char c)
        {
            if (c >= ' ')
            {
                SetCharacter(CursorPos, c, CurrentTextColor, CurrentBackground, CurrentAttribute);
                AdvanceCursor();
            }
            else
            if (c == '\r')
                PrintReturn();
            else
                if (c == '\n')
                PrintLineFeed();
        }

        public void AdvanceCursor()
        {
            if (LineWrap)
                CursorPos++;
            else
                CurrentColumn++;
        }

        public void CopyCharacter(int dest, int src)
        {
            CharacterData[dest] = CharacterData[src];
            TextColorData[dest] = TextColorData[src];
            BackColorData[dest] = BackColorData[src];
            AttributeData[dest] = AttributeData[src];
        }

        public void SetCharacter(int pos, char c)
        {
            if (pos < 0 || pos >= CharacterData.Length)
                return;

            CharacterData[pos] = c;
            TextColorData[pos] = CurrentTextColor;
            BackColorData[pos] = CurrentBackground;
            AttributeData[pos] = CurrentAttribute;
        }

        public void SetCharacter(int row, int col, char c, CharacterCell.ColorCodes textColor, CharacterCell.ColorCodes backColor, CharacterCell.AttributeCodes attribute)
        {
            int pos = GetPos(row, col);
            SetCharacter(pos, c, textColor, backColor, attribute);
        }

        public void SetCharacter(int pos, char c, CharacterCell.ColorCodes textColor, CharacterCell.ColorCodes backColor, CharacterCell.AttributeCodes attribute)
        {
            if (pos < 0 || pos >= CharacterData.Length)
                return;

            CharacterData[pos] = c;
            TextColorData[pos] = textColor;
            BackColorData[pos] = backColor;
            AttributeData[pos] = attribute;
        }


        public void SetCharacter(char c)
        {
            SetCharacter(CursorPos, c);
        }

        private void SetCharacter(int row, int col, char c)
        {
            int pos = GetPos(row, col);
            SetCharacter(pos, c);
        }

        public void PrintClear(string s, bool NewLine = true)
        {
            CurrentColumn = 0;
            ClearCurrentLine(true, true);
            Print(s);
            if (NewLine)
                PrintLine();
        }

        public void Print(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Print(s[i]);
            }
        }

        public void PrintAtStart(string s, bool NewLine = true)
        {
            if (CurrentColumn > 0)
                PrintLine();
            Print(s);
            if (NewLine)
                PrintLine();
        }

        /// <summary>
        /// Prints a series of objects. All objects will be converted to a string with ToString(). If the last object is null, 
        /// NewLine will be suppressed.
        /// </summary>
        /// <param name="s"></param>
        public void Print(string[] s)
        {
            foreach (var item in s)
            {
                Print(item.ToString());
            }
        }

        public void PrintLine(string s)
        {
            Print(s);
            PrintLine();
        }

        public void PrintLineFeed()
        {
            if (CurrentRow >= Rows - 1)
            {
                ScrollUp();
                CurrentRow = Rows - 1;
            }
            else
                MoveDown();
        }

        public void PrintReturn()
        {
            CurrentColumn = 0;
        }

        public void PrintLine()
        {
            PrintReturn();
            PrintLineFeed();
        }

        private SizeF MeasureFont(Font font, Graphics g)
        {
            SizeF size = g.MeasureString(MEASURE_STRING, font, int.MaxValue, StringFormat.GenericTypographic);
            size.Width = size.Width / MEASURE_STRING.Length;
            return size;
        }

        private Font GetBestFont()
        {
            int testSize = 24;

            Font useFont = null;
            float ch = (float)ClientRectangle.Height / (float)(Rows + 1);
            if (RowHeight < 8)
            {
                RowHeight = 8;
            }

            string[] fonts = new[] {
                "Classic Console",
                "Consolas",
                "Lucida Console",
                "Monospace"
            };

            foreach (string f in fonts)
            {
                using (Font testFont = new Font(
                                           f,
                                           testSize,
                                           FontStyle.Regular,
                                           GraphicsUnit.Pixel))
                {
                    if (testFont.Name == f)
                    {
                        useFont = new Font(f, testSize, FontStyle.Regular, GraphicsUnit.Pixel);
                        break;
                    }
                    else
                    {
                    }
                }
            }
            if (useFont == null)
            {
                useFont = new Font(Font.FontFamily, testSize, FontStyle.Regular);
            }

            Graphics g = CreateGraphics();

            // Measure a known font size so we can figure out the character dimensions
            SizeF fs = MeasureFont(useFont, g);
            float ratio = ch / fs.Height;
            float newSize = testSize * ratio;
            useFont = new Font(useFont.FontFamily, newSize, FontStyle.Regular, GraphicsUnit.Pixel);

            // Measure and set the size of a character cell based on the new font's character size.
            fs = MeasureFont(useFont, g);
            RowHeight = fs.Height;
            ColWidth = fs.Width + 1;

            return useFont;
        }

        public void Clear()
        {
            Fill(' ');
            CurrentColumn = 0;
            CurrentRow = 0;
        }

        private void DisplayControl_Paint(object sender, PaintEventArgs e)
        {
            UpdateTextCursorMode();

            if (!FontValid)
            {
                Font = GetBestFont();
                FontValid = true;
            }

            Graphics g = e.Graphics;

            g.Clear(Color.Black);

            float right = (Columns + 1) * ColWidth;
            float bottom = (Rows + 1) * RowHeight;
            float x = 0;
            float y = 0;

            float xo = 0;
            if (right < ClientRectangle.Width)
                xo = (ClientRectangle.Width - right) / 2;

            float yo = 0;

            g.TranslateTransform(xo, yo);

            g.DrawRectangle(borderPen, -2, -2, right + 4, bottom + 4);

            g.FillRectangle(Brushes[(int)CurrentBackground], 0, 0, right, bottom);
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    int pos = GetPos(row, col);
                    x = col * ColWidth;
                    y = row * RowHeight;

                    CharacterCell.ColorCodes color = BackColorData[pos];
                    Brush b = Brushes[(int)TextColorData[pos]];
                    Brush bg = Brushes[(int)BackColorData[pos]];
                    if (color != CurrentBackground)
                    {
                        g.FillRectangle(bg, x, y, ColWidth, RowHeight);
                    }
                    if (AttributeData[pos].HasFlag(CharacterCell.AttributeCodes.Reverse))
                    {
                        g.FillRectangle(b, x, y, ColWidth, RowHeight);
                        b = bg;
                    }

                    if ((!CursorOn) || !(AttributeData[pos].HasFlag(CharacterCell.AttributeCodes.Blink)))
                    {
                        g.DrawString(CharacterData[pos].ToString(), Font, b, x, y, StringFormat.GenericTypographic);

                        if (AttributeData[pos].HasFlag(CharacterCell.AttributeCodes.Underline))
                        {
                            float h = RowHeight / 8;
                            g.FillRectangle(Brushes[(int)CurrentTextColor], x, y + (RowHeight - h), ColWidth, h);
                        }
                    }
                }
            }

            x = CurrentColumn * ColWidth - 1;
            y = CurrentRow * RowHeight;

            if (CursorOn)
            {
                switch (TextCursor)
                {
                    case TextCursorStyles.None:
                        break;
                    case TextCursorStyles.Underline:
                        float h = RowHeight / 4;
                        g.FillRectangle(Brushes[(int)CurrentTextColor], x, y + (RowHeight - h), ColWidth, h);
                        break;
                    case TextCursorStyles.Left:
                        g.FillRectangle(Brushes[(int)CurrentTextColor], x, y, ColWidth / 4, RowHeight);
                        break;
                    case TextCursorStyles.Block:
                    default:
                        g.FillRectangle(Brushes[(int)CurrentTextColor], x, y, ColWidth, RowHeight);
                        break;
                }
            }

            g.ResetTransform();
        }

        private void UpdateTextCursorMode()
        {
            if (CurrentDialog != null && CurrentDialog.Visible)
            {
                TextCursor = TextCursorStyles.None;
                return;
            }

            if (Terminal != null)
                Terminal.EchoMode = this.EchoMode;
            switch (EchoMode)
            {
                case EchoModes.LineEdit:
                    TextCursor = TextCursorStyles.Left;
                    break;
                case EchoModes.FullScreenEdit:
                    if (InsertMode == InsertKeyMode.Insert)
                        TextCursor = TextCursorStyles.Left;
                    else
                        TextCursor = TextCursorStyles.Block;
                    break;
                case EchoModes.EchoOff:
                case EchoModes.LocalEcho:
                default:
                    if (TextCursor != TextCursorStyles.None)
                        TextCursor = TextCursorStyles.Underline;
                    break;
            }
        }

        /// <summary>
        /// Makes the cursor blink ON. Sets the redraw counter to 0,
        /// so the cursor will blink on the next 1/60 second cycle.
        /// </summary>
        private void BlinkCursor()
        {
            CursorOn = false;
            NextDraw = 0;
        }

        /// <summary>
        /// Redraws the window when the RedrawFrames counter reaches 0.
        /// This will happen when the cursor blinks or when the cursor
        /// has moved (usually because the screen data has been updated.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (this.Terminal == null)
                return;

            NextDraw -= 1;
            if (NextDraw > 0)
            {
                return;
            }

            CursorOn = !CursorOn;
            Invalidate();
            NextDraw = BlinkInterval;
        }

        public void MoveUp()
        {
            CurrentRow -= 1;
        }

        public void MoveDown()
        {
            CurrentRow += 1;
        }

        public void MoveLeft()
        {
            CurrentColumn -= 1;
        }

        public void MoveRight()
        {
            CurrentColumn += 1;
        }

        private void ScrollUp()
        {
            AddLineToBuffer(0);

            int start = GetPos(1, 0);
            int end = GetPos(Rows, 0);
            for (int i = start; i < end; i++)
            {
                CopyCharacter(i - Columns, i);
            }
            FillRow(Rows - 1, ' ');
        }

        /// <summary>
        /// adds a line of text to the scroll buffer.
        /// </summary>
        /// <param name="v"></param>
        private void AddLineToBuffer(int Row)
        {
            StringBuilder s = new StringBuilder();
            for (int x = 0; x < Columns; x++)
            {
                s.Append(GetChar(Row, x));
            }
            Buffer.Add(s.ToString());
        }

        private void FillRow(int Row, char Value)
        {
            int pos = GetPos(Row, 0);
            for (int x = 0; x < Columns; x++)
            {
                SetCharacter(pos + x, Value, CurrentTextColor, CurrentBackground, CurrentAttribute);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            KeyEventArgs e = new KeyEventArgs(keyData);
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    HandleKeyDown(this, e);
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;  // used
        }

        private void DisplayControl_Load(object sender, EventArgs e)
        {

        }

        public void SaveScreen(ScreenBuffer buffer)
        {
            buffer = new ScreenBuffer(CharacterData.Length);
            buffer.Save(CharacterData, TextColorData, BackColorData, AttributeData);
        }

        public void RestoreScreen(ScreenBuffer buffer)
        {
            SaveBuffer.Load(CharacterData, TextColorData, BackColorData, AttributeData);
        }

        #endregion

    }
}
