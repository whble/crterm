using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRTERM.Common;

namespace CRTERM.UI
{
	public partial class PropertyPanel : UserControl
	{
		public PropertyPanel()
		{
			InitializeComponent();
		}

		ConfigList _configData = new ConfigList();
		public ConfigList ConfigData
		{
			get { return _configData; }
			set
			{
				_configData = value;
				LoadConfigData();
			}
		}

		void LoadConfigData()
		{
			this.Controls.Clear();
      if (ConfigData == null)
				return;

			int y = this.Padding.Top;
			int w = 120;
			foreach (ConfigItem item in ConfigData.Values)
			{
        Control newc;
        switch (item.ItemType)
        {
          case ConfigItem.ConfigItemTypes.List:
            ComboBox cbo = new ComboBox();
            cbo.Items.AddRange(item.PickList);
				    cbo.Text = item.Value;
            newc = cbo;
            break;
          case ConfigItem.ConfigItemTypes.YesNo:
            CheckBox ckb = new CheckBox();
            bool bVal;
            Boolean.TryParse(item.Value, out bVal);
            ckb.Checked = bVal;
            newc = ckb;
            break;
          default:
            newc = new TextBox();
            newc.Text = item.Value;
            break;
        }
        newc.Tag = item.Name;
        newc.Left = w + 1;
        newc.Width = this.ClientRectangle.Width - newc.Left - this.Padding.Left - this.Padding.Right;
        newc.Top = y;
        //t.BorderStyle = System.Windows.Forms.BorderStyle.None;
        newc.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
        Controls.Add(newc);
        
        Label l = new Label();
				l.Text = item.Name;
				l.AutoSize = false;
				l.Top = y;
				l.Width = w;
				l.TextAlign = ContentAlignment.MiddleRight;
        l.Height = newc.Height;
				Controls.Add(l);

				y += newc.Height + newc.Margin.Top + newc.Margin.Bottom;
			}
		}

    public void SaveConfiguration()
    {
      foreach (Control control in Controls)
      {
        string name = control.Tag as string; 
        if(name != null && ConfigData.ContainsKey(name)) {
          ConfigData[name].Value=control.Text;
        }
      }
    }
	}
}
