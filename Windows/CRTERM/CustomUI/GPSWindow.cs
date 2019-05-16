using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRTERM.CustomUI
{
	public partial class GPSWindow : Form
	{
		public GPSWindow()
		{
			InitializeComponent();
		}

		private Terminal.ITerminal _terminal;
		public Terminal.ITerminal Terminal
		{
			get { return _terminal; }
			set {
				if (value == _terminal)
					return;
				// Disconnect events
				if (_terminal != null)
				{
					_terminal.TextReceived -= _terminal_TextReceived;
				}
				_terminal = value; 
				// Connect events
				if(_terminal != null) {
					_terminal.TextReceived += new Common.TextReceivedEventHandler(_terminal_TextReceived);
				}
			}
		}

		void _terminal_TextReceived(ICommProvider sender, string Text)
		{
			FixString.Text = Text;
		}
	}
}
