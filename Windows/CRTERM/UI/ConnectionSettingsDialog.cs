using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRTERM.Transport;
using CRTERM.Common;
using CRTERM.Modem;
using CRTERM.Terminal;

namespace CRTERM.UI
{
	public partial class ConnectionSettingsDialog : Form
	{
		public ConnectionSettingsDialog()
		{
			InitializeComponent();
		}

		private Connection _connection = null;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Connection SelectedConnection
		{
			get { return _connection; }
			set
			{
				_connection = value;
				if (value != null)
				{
					LoadConfiguration(SelectedConnection);
				}
			}
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
			SaveConfiguration();
			Close();
		}

		private void LoadConfiguration(Connection conn)
		{
			NameText.Text = conn.Name;
			TransportButtons.Options = conn.GetProviderNames<ITransport>();
			TransportButtons.Text = conn.Transport.GetType().Name;
			ModemButtons.Options = conn.GetProviderNames<IModem>();
			ModemButtons.Text = conn.Modem.GetType().Name;
			EmulatorButtons.Options = conn.GetProviderNames<ITerminal>();
			EmulatorButtons.Text = conn.Terminal.GetType().Name;
		}

		void SaveConfiguration()
		{
			SelectedConnection.Name = NameText.Text;
			TransportProperties.SaveConfiguration();
			ModemProperties.SaveConfiguration();
			EmulatorProperties.SaveConfiguration();
		}

		private void SaveSettings()
		{
		}

		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}

		private void ConnectionSettingsDialog_Load(object sender, EventArgs e)
		{
		}

		public string ConnectionName
		{
			get { return NameText.Text; }
			set { NameText.Text = value; }
		}

		private void TransportButtons_TextChanged(object sender, EventArgs e)
		{
			GetTransportConfiguration(TransportButtons.Text);
		}

		private void ModemButtons_TextChanged(object sender, EventArgs e)
		{
			GetModemConfiguration(ModemButtons.Text);
		}

		private void EmulatorButtons_TextChanged(object sender, EventArgs e)
		{
			GetEmulatorConfiguration(EmulatorButtons.Text);
		}

		void GetTransportConfiguration(string TransportName)
		{
			SelectedConnection.SelectTransport(TransportName);
			if (SelectedConnection.Transport != null)
				TransportProperties.ConfigData = SelectedConnection.Transport.ConfigData;
			else
				TransportProperties.ConfigData = null;
		}

		private void GetModemConfiguration(string ModemName)
		{
			SelectedConnection.SelectModem(ModemName);
			if (SelectedConnection.Modem != null)
				ModemProperties.ConfigData = SelectedConnection.Modem.ConfigData;
			else
				ModemProperties.ConfigData = null;
		}

		private void GetEmulatorConfiguration(string EmulatorName)
		{
			SelectedConnection.SelectTerminal(EmulatorName);
			if (SelectedConnection.Terminal != null)
				EmulatorProperties.ConfigData = SelectedConnection.Terminal.ConfigData;
			else
				EmulatorProperties.ConfigData = null;
		}

	}
}
