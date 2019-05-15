using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CRTerm
{
    public class FrameBufferControl_oops : System.Windows.Forms.Control
    {
        Font TextFont = new Font("Terminal", 12);
        Brush TextBrush = new SolidBrush(Color.LightGreen);
        
        Timer timer = new Timer();

        /// <summary>
        /// Screen character data. Data is addressed as Data[Row, Col].
        /// </summary>
        public char[,] CharacterData = null;
        public ColorCodes[,] ForegroundColorData = null;
        public ColorCodes[,] BackgroundColorData = null;

        /// <summary>
        /// Column of the cursor position. 0 is left edge
        /// </summary>
        int CurrentCol = 0;
        /// <summary>
        /// Row of cursor position. 0 is top of the screen
        /// </summary>
        int CurrentRow = 0;
        ColorCodes _currentForeground = ColorCodes.Green | ColorCodes.Light;
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

        int _cols = 80;
        public int Cols
        {
            get { return _cols; }
            protected set { _cols = value; }
        }

        int _rows = 24;
        public int Rows
        {
            get { return _rows; }
            protected set { _rows = value; }
        }

        public FrameBufferControl_oops()
        {
            this.SetBufferSize(25, 80);
            this.Paint += new PaintEventHandler(FrameBufferControl_Paint);
            timer.Tick += new EventHandler(timer_Tick);
            this.VisibleChanged += new EventHandler(FrameBufferControl_VisibleChanged);
        }

        public virtual void SetBufferSize(int Rows, int Cols)
        {
            this._cols = Cols;
            this._rows = Rows;
            CharacterData = new char[Rows, Cols];
            ForegroundColorData = new ColorCodes[Rows, Cols];
            BackgroundColorData = new ColorCodes[Rows, Cols];
        }

        public virtual void Print(char c)
        {
            CharacterData[CurrentRow, CurrentCol] = c;
            CurrentCol += 1;
            if (CurrentCol > Cols)
            {
                CurrentCol = 0;
                CurrentRow += 1;
            }

            if (CurrentRow > Rows)
            {
                for (int row = 0; row < Rows - 1; row++)
                {
                    for (int col = 0; col < Cols; col++)
                    {
                        CharacterData[row, col] = CharacterData[row + 1, col];
                        ForegroundColorData[row, col] = ForegroundColorData[row + 1, col];
                        BackgroundColorData[row, col] = BackgroundColorData[row + 1, col];
                    }
                }
            }

            for (int col = 0; col < Cols; col++)
            {
                CharacterData[Rows - 1, col] = ' ';
                ForegroundColorData[Rows - 1, col] = _currentForeground;
                BackgroundColorData[Rows - 1, col] = _currentBackground;
            }
        }

        public virtual void Print(string s)
        {
            for(int i=0; i<s.Length; i++)
            {
                Print(s.Substring(i, 1));
            }
        }

        public virtual void Locate(int Row, int Col)
        {
            CurrentRow = Row;
            CurrentCol = Col;

            if (Row < 0)
                Row = 0;
            if (Row >= Rows)
                Row = Rows - 1;
            if (Col < 0)
                Col = 0;
            if (Col >= Cols)
                Col = Cols - 1;
        }

        public virtual void Clear()
        {
            Fill(' ');
            Locate(0, 0);
        }

        public virtual void Fill(char c)
        {
            if (CurrentRow > Rows)
            {
                for (int row = 0; row < Rows - 1; row++)
                {
                    for (int col = 0; col < Cols; col++)
                    {
                        CharacterData[row, col] = c;
                        ForegroundColorData[row, col] = _currentForeground;
                        BackgroundColorData[row, col] = _currentBackground;
                    }
                }
            }
        }

        /// <summary>
        /// Draw the frame buffer to the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrameBufferControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int colWidth = 8;
            int rowHeight = 19;

            g.Clear(Color.Black);
            for (int row = 0; row < Rows - 1; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                 
                    g.DrawString(CharacterData[row, col].ToString(), TextFont, TextBrush, col * colWidth, row * rowHeight);
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        void FrameBufferControl_VisibleChanged(object sender, EventArgs e)
        {
            timer.Enabled = this.Visible;
        }

    }
}
