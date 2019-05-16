using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRTERM.Terminal;
using CRTERM.UI;
using CRTERM.Common;

namespace CRTERM.UI
{
	public partial class TerminalDisplay : UserControl
	{
		public TerminalDisplay()
		{
			InitializeComponent();
		}

		bool dataChanged = true;
		bool cursorLit;
		private void CursorTimer_Tick(object sender, EventArgs e)
		{
			FlushBuffer();
			cursorLit = !cursorLit;
			Refresh();
		}

		private void FlushBuffer()
		{
			if (CurrentConnection == null)
				return;
			if (CurrentConnection.Transport == null)
				return;
			//Connection.Transport.FlushBuffer();
		}

		Font textFont = new Font("Lucida Console", 10);
		Brush textBrush = Brushes.White;
		Brush backgroundBrush = Brushes.Black;

		private FrameBuffer Buffer = new FrameBuffer();
		Connection _connection = null;
		public Connection CurrentConnection
		{
			get { return _connection; }
			set
			{
				if (CurrentConnection != null)
				{
					if (CurrentConnection.Terminal != null)
						CurrentConnection.Terminal.TerminalEvent -= Terminal_TerminalEvent;
				}
				_connection = value;
				if (value != null)
				{
					System.Diagnostics.Debug.WriteLine("TerminalDisplay Connection Changed: "
						+ value.Name + ", Transport: " + value.Transport.GetType().Name + ", Terminal: " + value.Terminal.GetType().Name);
					this.Buffer.Terminal = CurrentConnection.Terminal;
					if (CurrentConnection.Terminal != null)
						CurrentConnection.Terminal.TerminalEvent += new TerminalEventHandler(Terminal_TerminalEvent);
				}
			}
		}

		void Terminal_TerminalEvent(ICommProvider sender, TerminalEventArgs e)
		{
			switch (e.EventType)
			{
				case EventTypeCodes.Connected:
					Buffer.PrintString("Connected");
					Buffer.NewLine();
					break;
				case EventTypeCodes.Disconnected:
					Buffer.PrintString("Disconnected: ");
					Buffer.PrintString(e.Message);
					Buffer.NewLine();
					break;
			}
		}

		RectangleF textCursor = new RectangleF(0, 0, 8, 12);
		PointF cursorMargin = new PointF(0, 0);

		/// <summary>
		/// Redraw entire terminal window
		/// </summary>
		public void DrawText(Graphics g)
		{
			g.Clear(BackColor);
			for (int row = 0; row < Buffer.Rows; row++)
			{
				for (int col = 0; col < Buffer.Columns; col++)
				{
					DrawCharAt(g, textBrush, row, col);
				}
			}
		}

		/// <summary>
		/// Redraw the character at the cursor position
		/// </summary>
		public void DrawCharAtCursor(Graphics g)
		{
			DrawCharAtCursor(g, textBrush);
		}

		public void DrawCharAtCursor(Graphics g, Brush DrawBrush)
		{
			DrawCharAt(g, DrawBrush, Buffer.CursorRow, Buffer.CursorCol);
		}

		private void DrawCharAt(Graphics g, Brush DrawBrush, int Row, int Col)
		{
			PointF pos = new PointF(Col * textCursor.Width, Row * textCursor.Height);
			pos.X -= cursorMargin.X;
			string s = Buffer.GetChar(Row, Col).ToString();
			g.DrawString(s, textFont, DrawBrush, pos);
		}

		private void GetCharSize(out RectangleF CharSize, out PointF Margin)
		{
			Graphics g = this.CreateGraphics();
			// DrawString and MeasureString leave some margin.
			// by drawing two characters, we can deduce the margin
			// and subtract it out.
			SizeF s = g.MeasureString("X", textFont);
			SizeF s2 = g.MeasureString("XX", textFont);
			CharSize = new RectangleF(0, 0, s2.Width - s.Width, s.Height);
			Margin = new PointF((s.Width - CharSize.Width) / 2, 0);
		}

		/// <summary>
		/// Redraw the cursor and the character at the cursor position
		/// </summary>
		public void DrawCursor(Graphics g, bool CursorLit)
		{
			Brush tBrush = CursorLit ? backgroundBrush : textBrush;
			Brush cBrush = cursorLit ? textBrush : backgroundBrush;

			textCursor.X = Buffer.CursorCol * textCursor.Width;
			textCursor.Y = Buffer.CursorRow * textCursor.Height;
			g.FillRectangle(cBrush, textCursor);
			DrawCharAtCursor(g, tBrush);
		}

		private void TerminalDisplay_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				this.textBrush = new SolidBrush(this.ForeColor);
				this.backgroundBrush = new SolidBrush(this.BackColor);
				GetCharSize(out textCursor, out cursorMargin);
				EnableTimers(true);
			}
		}

		void Buffer_TextChanged(object sender, FrameBuffer.TextChangedEventArgs e)
		{
			Refresh();
		}

		private void TerminalDisplay_Paint(object sender, PaintEventArgs e)
		{
			//diable under normal circumstances
			//EnableTimers(false);

			Graphics g = e.Graphics;
			if (Buffer != null)
			{
				DrawText(g);
				DrawCursor(g, cursorLit);
			}
			else
				DrawBlank(g);
		}

		void DrawBlank(Graphics g)
		{
			g.Clear(BackColor);
			g.DrawString("Please open a connection.", textFont, textBrush, new PointF(0, 0));
		}

		public void HandleKeyPress(object sender, KeyPressEventArgs e)
		{
			CurrentConnection.Terminal.KeyPress(e);
		}

		public void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (CurrentConnection == null)
				return;

			if (e.KeyCode == Keys.Pause)
				CurrentConnection.Transport.Break();
			CurrentConnection.Terminal.KeyDown(e);
		}

		private void redrawTimer_Tick(object sender, EventArgs e)
		{
			if (dataChanged)
			{
				this.Invalidate();
			}
		}

		/// <summary>
		/// Allows the screen to be refreshed at the next redraw interval. This avoids
		/// a costly redraw every single time a character is drawn to the screen.
		/// </summary>
		public override void Refresh()
		{
			dataChanged = true;
		}

		public void DisableTimers()
		{
			EnableTimers(false);
		}

		public void EnableTimers(bool Enabled)
		{
			cursorTimer.Enabled = Enabled;
			redrawTimer.Enabled = Enabled;
		}
	}
}
