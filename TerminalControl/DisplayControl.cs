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
        #region Private Fields
        private static string MEASURE_STRING = new string('W', 80);

        private int _cols;
        private int _rows;
        private float RowHeight = 16;
        private float ColWidth = 8;
        private int x;
        private int y;
        public TextCursorStyles TextCursor { get; set; }
        private InsertKeyMode _insertMode = InsertKeyMode.Overwrite;

        private EchoModes _echoMode;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool AddLinefeed { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool LineWrap { get; set; }
        private string _statusText;

        protected bool FontValid = false;
        private Timer drawTimer = new Timer();

        public bool CursorEnabled;
        public bool CursorOn;
        public int NextDraw = 0;
        public int BlinkInterval = 20;

        public CharacterCell.ColorCodes CurrentBackground { get; set; }
        public CharacterCell.ColorCodes CurrentTextColor { get; set; }
        public CharacterCell.Attributes CurrentAttribute { get; set; }

        private IEditorPlugin _editor = null;
        private ITerminal _terminal = new Terminals.ANSITerminal();
        #endregion

        #region Public Properties
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public List<string> Buffer = new List<string>();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public CharacterCell[] CharacterData = new CharacterCell[2000];

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
                    x = Columns - 1;
                }

                BlinkCursor();
            }
        }

        public char CharUnderCursor
        {
            get
            {
                CharacterCell cell = CharacterData[CursorPos];
                if (cell == null)
                {
                    return '\0';
                }

                if (cell.Value.Length < 1)
                {
                    return '\0';
                }

                return cell.Value[0];
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
                CharacterData[pos] = CharacterData[pos - 1].Copy();
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
                CharacterData[pos - 1] = CharacterData[pos].Copy();
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
                SetCharacter(i, " ", CurrentTextColor, CurrentBackground, CurrentAttribute);
            }
        }

        public int GetPos(int row, int col)
        {
            return row * Columns + col;
        }


        public static Brush[] Brushes = new SolidBrush[]
        {
            new SolidBrush(Color.FromArgb(0,0,0)),
            new SolidBrush(Color.FromArgb(0,0,128)),
            new SolidBrush(Color.FromArgb(0,128,0)),
            new SolidBrush(Color.FromArgb(0,128,128)),
            new SolidBrush(Color.FromArgb(128,0,0)),
            new SolidBrush(Color.FromArgb(0,0,128)),
            new SolidBrush(Color.FromArgb(0,128,128)),
            new SolidBrush(Color.FromArgb(128,128,128)),
            new SolidBrush(Color.FromArgb(64,64,64)),
            new SolidBrush(Color.FromArgb(0,0,255)),
            new SolidBrush(Color.FromArgb(0,255,0)),
            new SolidBrush(Color.FromArgb(0,255,255)),
            new SolidBrush(Color.FromArgb(255,0,0)),
            new SolidBrush(Color.FromArgb(0,0,255)),
            new SolidBrush(Color.FromArgb(0,255,255)),
            new SolidBrush(Color.FromArgb(255,255,255)),
        };

        public DisplayControl()
        {
            InitializeComponent();

            CurrentTextColor = CharacterCell.ColorCodes.Gray;
            SetTextMode(36, 80);
            DoubleBuffered = true;

            drawTimer.Interval = 1000 / 60;
            drawTimer.Tick += DrawTimer_Tick;
            drawTimer.Enabled = true;

            Resize += TerminalControl_Resize;
            KeyDown += HandleKeyDown;
            KeyPress += HandleKeyPress;
            KeyUp += HandleKeyUp;
        }

        private void TerminalControl_Resize(object sender, EventArgs e)
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
            CharacterData = new CharacterCell[Rows * Columns];
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

        public void Fill(string c)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    SetCharacter(row, col, c, CurrentTextColor, CurrentBackground, CharacterCell.Attributes.Normal);
                }
            }
        }

        public void HandleKeyDown(object sender, KeyEventArgs e)
        {
            bool handled = true;
            switch (EchoMode)
            {
                case EchoModes.LineEdit:
                    break;
                case EchoModes.FullScreen:
                    handled = HandleFullScreenKey(e);
                    break;
                case EchoModes.Plugin:
                    handled = true;
                    Editor?.HandleKeyDown(sender, e);
                    break;
                case EchoModes.EchoOff:
                case EchoModes.LocalEcho:
                default:
                    if (e.KeyCode == Keys.Apps)
                    {
                        EchoMode = EchoModes.FullScreen;
                        InsertMode = InsertKeyMode.Overwrite;
                        BlinkCursor();
                    }
                    else
                        handled = false;
                    break;
            }

            if (!handled)
            {
                TerminalKeyEventArgs k = new TerminalKeyEventArgs(e);
                Terminal.SendKey(k);
            }
        }

        private bool HandleFullScreenKey(KeyEventArgs e)
        {
            bool handled = true;
            switch (e.KeyCode)
            {
                case Keys.Apps:
                    EchoMode = EchoModes.EchoOff;
                    CurrentColumn = 0;
                    CurrentRow = Rows - 1;
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
                        while (CurrentColumn > 0 && CharacterData[CursorPos - 1].Value == " ")
                            CurrentColumn -= 1;
                    }
                    break;
                default:
                    handled = false;
                    break;
            }
            return handled;
        }

        private string GetChar(int Row, int Col)
        {
            int pos = GetPos(Row, Col);
            return CharacterData[pos].Value;
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
                case EchoModes.FullScreen:
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
                SetCharacter(CurrentRow, CurrentColumn, c.ToString(), CurrentTextColor, CurrentBackground, CurrentAttribute);
                AdvanceCursor();
            }
            else if (c == '\r')
                PrintReturn();
            else if (c == '\n')
                PrintLineFeed();
        }

        public void AdvanceCursor()
        {
            CurrentColumn++;
            if (CurrentColumn >= Columns && LineWrap == true)
            {
                CurrentColumn = 0;
                CurrentRow++;
            }
        }

        public void SetCharacter(char c)
        {
            int pos = CursorPos;
            CharacterData[pos] = new CharacterCell
            {
                Value = c.ToString()
            };
        }

        private void SetCharacter(int row, int col, char c)
        {
            int pos = row * Columns + col;
            if (pos < 0 || pos >= CharacterData.Length)
            {
                return;
            }

            if (CharacterData[pos] == null)
            {
                CharacterData[pos] = new CharacterCell();
            }

            CharacterData[pos].Value = c.ToString();
            CharacterData[pos].TextColor = CurrentTextColor;
            CharacterData[pos].BackColor = CurrentBackground;
            CharacterData[pos].Attribute = CurrentAttribute;
        }

        public void SetCharacter(
            int pos,
            string c,
            CharacterCell.ColorCodes textColor,
            CharacterCell.ColorCodes backColor,
            CharacterCell.Attributes attribute)
        {
            if (pos < 0 || pos >= CharacterData.Length)
            {
                return;
            }

            if (CharacterData[pos] == null)
            {
                CharacterData[pos] = new CharacterCell();
            }

            CharacterData[pos].Value = c.ToString();
            CharacterData[pos].TextColor = textColor;
            CharacterData[pos].BackColor = backColor;
            CharacterData[pos].Attribute = attribute;
        }

        public void SetCharacter(
            int row,
            int col,
            string c,
            CharacterCell.ColorCodes textColor,
            CharacterCell.ColorCodes backColor,
            CharacterCell.Attributes attribute)
        {
            int pos = row * Columns + col;
            if (pos < 0 || pos >= CharacterData.Length)
            {
                return;
            }

            if (CharacterData[pos] == null)
            {
                CharacterData[pos] = new CharacterCell();
            }

            CharacterData[pos].Value = c.ToString();
            CharacterData[pos].TextColor = textColor;
            CharacterData[pos].BackColor = backColor;
            CharacterData[pos].Attribute = attribute;
        }

        //public void Print(char[] c)
        //{
        //    for (int i = 0; i < c.Length; i++)
        //    {
        //        Print(c[i]);
        //    }
        //}

        public void PrintFirst(string s, bool NewLine = true)
        {
            if (CurrentColumn > 0)
                PrintLine();
            Print(s);
            if (NewLine)
                PrintLine();
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
                SetCharacter(CurrentRow, CurrentColumn, s.Substring(i, 1), CurrentTextColor, CurrentBackground, CurrentAttribute);
                AdvanceCursor();
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

            string[] fonts = new[]
            {
                "Classic Console",
                "Consolas",
                "Lucida Console",
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
            Fill(" ");
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

            float right = (Columns + 1) * ColWidth;
            float bottom = (Rows + 1) * RowHeight;
            float x = 0;
            float y = 0;

            float xo = 0;
            if (right < ClientRectangle.Width)
                xo = (ClientRectangle.Width - right) / 2;

            float yo = 0;

            g.TranslateTransform(xo, yo);

            g.FillRectangle(Brushes[(int)CurrentBackground], 0, 0, right, bottom);
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    CharacterCell cc = CharacterData[row * Columns + col];
                    if (cc == null)
                    {
                        continue;
                    }

                    x = col * ColWidth;
                    y = row * RowHeight;

                    CharacterCell.ColorCodes color = cc.BackColor;
                    Brush b = Brushes[(int)cc.TextColor];
                    if (color != CurrentBackground)
                    {
                        g.FillRectangle(Brushes[(int)cc.BackColor], x, y, ColWidth, RowHeight);
                    }
                    if (cc.Attribute.HasFlag(CharacterCell.Attributes.Reverse))
                    {
                        g.FillRectangle(Brushes[(int)cc.TextColor], x, y, ColWidth, RowHeight);
                        b = Brushes[(int)cc.BackColor];
                    }

                    g.DrawString(cc.Value, Font, b, x, y, StringFormat.GenericTypographic);
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
            if (Terminal != null)
                Terminal.EchoMode = this.EchoMode;
            switch (EchoMode)
            {
                case EchoModes.LineEdit:
                    TextCursor = TextCursorStyles.Left;
                    break;
                case EchoModes.FullScreen:
                    if (InsertMode == InsertKeyMode.Insert)
                        TextCursor = TextCursorStyles.Left;
                    else
                        TextCursor = TextCursorStyles.Block;
                    break;
                case EchoModes.EchoOff:
                case EchoModes.LocalEcho:
                default:
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
                CharacterData[i - Columns] = CharacterData[i].Copy();
            }
            FillRow(Rows - 1, " ");
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

        private void FillRow(int Row, string Value)
        {
            for (int col = 0; col < Columns; col++)
            {
                SetCharacter(Row, col, Value, CurrentTextColor, CurrentBackground, CurrentAttribute);
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

        #endregion
    }
}
