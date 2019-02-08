using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TerminalUI.Terminals;

namespace TerminalUI
{
    public partial class DisplayControl : UserControl
    {
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool AddLinefeed { get; set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool BackspaceDelete { get; set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool BackspaceOverwrite { get; set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool BackspacePull { get; set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool LineWrap { get; set; }

        protected bool FontValid = false;
        private Timer drawTimer = new Timer();

        public bool CusorEnabled;
        public bool CursorOn;
        public int NextDraw = 0;
        public int BlinkInterval = 20;

        public CharacterCell.ColorCodes CurrentBackground { get; set; }
        public CharacterCell.ColorCodes CurrentTextColor { get; set; }
        public CharacterCell.Attributes CurrentAttribute { get; set; }

        private IEditorPlugin _editor = null;

        private ITerminal _terminal = new Terminals.ANSITerminal();
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

        public void MoveToSOL()
        {
            CurrentColumn = 0;
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

        public CharacterCell[] CharacterData = new CharacterCell[2000];

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

            CurrentTextColor = CharacterCell.ColorCodes.Green;
            InitCharacterData(25, 80);
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

        private void InitCharacterData(int Rows, int Columns)
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

        public void ClearCursorToEnd()
        {
            throw new NotImplementedException();
        }

        public void ClearTopToCursor()
        {
            throw new NotImplementedException();
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
            bool handled = false;
            switch (EchoMode)
            {
                case EchoModes.LineEdit:
                    break;
                case EchoModes.FullScreen:
                    break;
                case EchoModes.Plugin:
                    handled = true;
                    Editor?.HandleKeyDown(sender, e);
                    break;
                case EchoModes.None:
                case EchoModes.LocalEcho:
                default:
                    break;
            }

            if (!handled)
            {
                TerminalKeyEventArgs k = new TerminalKeyEventArgs(e);
                Terminal.SendKey(k);
            }
        }

        public void HandleKeyPress(object sender, KeyPressEventArgs e)
        {
            bool handled = false;
            switch (EchoMode)
            {
                case EchoModes.None:
                    break;
                case EchoModes.LocalEcho:
                    Terminal.ProcessReceivedCharacter(e.KeyChar);
                    break;
                case EchoModes.LineEdit:
                    break;
                case EchoModes.FullScreen:
                    break;
                case EchoModes.Plugin:
                    handled = true;
                    Editor?.HandleKeyPress(sender, e);
                    break;
                default:
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
            SetCharacter(CurrentRow, CurrentColumn, c.ToString(), CurrentTextColor, CurrentBackground, CurrentAttribute);
            AdvanceCursor();
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
            if (CurrentRow + 1 >= Rows)
            {
                ScrollUp();
            }
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
                    if (color != CurrentBackground)
                    {
                        g.FillRectangle(Brushes[(int)cc.BackColor], x, y, ColWidth, RowHeight);
                    }

                    g.DrawString(cc.Value, Font, Brushes[(int)cc.TextColor], x, y, StringFormat.GenericTypographic);
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
                case EchoModes.None:
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
            for (int i = Columns; i < CharacterData.Length; i++)
            {
                CharacterData[i - Columns] = CharacterData[i];
            }

            FillRow(Rows - 1, " ");
        }

        private void FillRow(int Row, string Value)
        {
            for (int col = CharacterData.Length - Columns + 1; col < CharacterData.Length; col++)
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
    }
}
