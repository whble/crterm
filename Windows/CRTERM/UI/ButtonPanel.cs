using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRTERM.UI
{
	public partial class ButtonPanel : UserControl
	{
		public ButtonPanel()
		{
			InitializeComponent();
		}

		List<string> _options = null;
		public List<string> Options
		{
			set
			{
				this._options = value;
				this.Controls.Clear();
				if (value == null)
					return;
				foreach (string name in _options)
				{
					RadioButton b = new RadioButton();
					b.Text = name;
					b.CheckedChanged += new EventHandler(b_CheckedChanged);
					b.Dock = DockStyle.Left;
					this.Controls.Add(b);
				}
			}
		}

		void b_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton c = sender as RadioButton;
			if (c != null && c.Text != this.Text)
				this.Text = c.Text;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				foreach (Control c in this.Controls)
				{
					RadioButton b = c as RadioButton;
					if (b != null)
					{
						if (value == b.Text)
						{
							b.Checked = true;
						}
					}
				}
			}
		}

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		new public event EventHandler TextChanged
		{
			add { base.TextChanged += value; }
			remove { base.TextChanged -= value; }
		}
	}
}
