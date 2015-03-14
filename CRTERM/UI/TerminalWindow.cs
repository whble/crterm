using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRTERM.Common;

namespace CRTERM.UI
{
	public partial class TerminalWindow : Form
	{
		public TerminalWindow()
		{
			InitializeComponent();
		}

		public 

		/// <summary>
		/// Instantiate a terminal window and open a connection from a file.
		/// </summary>
		/// <param name="FileName"></param>
		public TerminalWindow(string FileName)
			: this()
		{
			if (System.IO.File.Exists(FileName))
				LoadFile(FileName);
		}

		private Connection _connection = null;
		public Connection CurrentConnection
		{
			get { return _connection; }
			set
			{
				if (_connection != null)
				{
					_connection.Disconnect();
				}
				_connection = value;
				TermDisplay.CurrentConnection = this.CurrentConnection;
        if (CurrentConnection != null)
        {
          this.Text = "CRTERM - " + CurrentConnection.Name;
        }
			}
		}

		private void TerminalWindow_KeyPress(object sender, KeyPressEventArgs e)
		{
			TermDisplay.HandleKeyPress(sender, e);
		}

		private void TerminalWindow_KeyDown(object sender, KeyEventArgs e)
		{
			TermDisplay.HandleKeyDown(sender, e);
		}

		private void connectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				CurrentConnection.Transport.Open();
			}
			catch (NullReferenceException)
			{
			}
		}

		private void TerminalWindow_Load(object sender, EventArgs e)
		{
			LoadSettings();
			TermDisplay.Focus();
			if (CurrentConnection != null)
				CurrentConnection.Transport.Open();
		}

		private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CurrentConnection.Transport.Close();
		}

		private void openTransportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CurrentConnection.Transport.Open();
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CurrentConnection.Transport.Close();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void connectionPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditConnection(CurrentConnection);
		}

		private void EditConnection(Connection SelectedConnection)
		{
			if (System.Diagnostics.Debugger.IsAttached)
				TermDisplay.DisableTimers();
			Application.DoEvents();

			ConnectionSettingsDialog dialog = new ConnectionSettingsDialog();
			dialog.SelectedConnection = SelectedConnection;
			DialogResult result = dialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				CurrentConnection = dialog.SelectedConnection;
			}
      if (CurrentConnection != null && !CurrentConnection.Connected)
        CurrentConnection.Connect();

			TermDisplay.EnableTimers(true);
		}

		private void newConnectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditConnection(new Connection());
		}

		private void openConnectionProfileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.Filter = Connection.filterString;
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					LoadFile(dialog.FileName);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error reading file.\r\n" + ex.Message);
			}
		}

		private void saveConnectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (CurrentConnection.FileName == "")
					saveConnectionAsToolStripMenuItem_Click(sender, e);
				else
					SaveConnection();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error writing \"" + CurrentConnection.FileName + "\"\r\n" + ex.Message);
			}
		}

		private void SaveConnection()
		{
			INIFile iniFile = new INIFile();
			CurrentConnection.Save(iniFile);
			iniFile.Save(CurrentConnection.FileName);
		}

		private void saveConnectionAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dialog = new SaveFileDialog();
			dialog.Filter = Connection.filterString;
			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				CurrentConnection.FileName = dialog.FileName;
				SaveConnection();
			}
		}

		void SaveSettings()
		{
			TermDisplay.EnableTimers(false);
			try
			{
				INIFile config = new INIFile();
				if (CurrentConnection != null)
				{
					CurrentConnection.Save(config);
					Properties.Settings.Default.LastConnection = config.Text;
					Properties.Settings.Default.Save();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		void LoadSettings()
		{
			INIFile config = new INIFile();
			config.Text = Properties.Settings.Default.LastConnection;
			CurrentConnection = new Connection(config);
		}

		private void TerminalWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSettings();
		}

		public void LoadFile(string FileName)
		{
			INIFile ini = new INIFile(FileName);
			Connection conn = new Connection(ini);
			conn.FileName = FileName;
			this.CurrentConnection = conn;
		}

    private void connectToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      CurrentConnection.Connect();
    }

    private void disconnectToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      CurrentConnection.Disconnect();
    }

		private void gPSToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

	}
}
