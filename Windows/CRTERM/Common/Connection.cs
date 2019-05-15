using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTERM.Transport;
using CRTERM.Modem;
using CRTERM.Terminal;
using CRTERM.Common;
using System.IO;

namespace CRTERM.Common
{
  /// <summary>
  /// Connection is responsible for saving the information necessary to
  /// connect to a specific remote system. This will save the configuration
  /// data for your transport and emulators.
  /// </summary>
  public class Connection
  {
    public Connection()
    {
      LoadDefaultSettings();
    }

    public Connection(INIFile ConfigFile)
    {
      LoadDefaultSettings();
      Load(ConfigFile);
    }

    private string _name = "New Connection";
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    public const string filterString = "Connections (*.ctc)|*.ctc|All Files (*.*)|*.*";
    private string _fileName = "";
    public string FileName
    {
      get { return _fileName; }
      set
      {
        _fileName = value;
        if (value != "")
          Name = System.IO.Path.GetFileNameWithoutExtension(_fileName);
      }
    }

    private ITransport _transport = null;
    public Transport.ITransport Transport
    {
      get { return _transport; }
      set
      {
        if (_transport != value)
        {
          _transport = value;
          SetEvents();
        }
      }
    }

    private IModem _modem = null;
    public IModem Modem
    {
      get { return _modem; }
      set
      {
        if (_modem != value)
        {
          _modem = value;
          SetEvents();
        }
      }
    }

    private ITerminal _emulator = null;
    public Terminal.ITerminal Terminal
    {
      get { return _emulator; }
      set
      {
        if (_emulator != value)
        {
          _emulator = value;
          SetEvents();
        }
      }
    }

    void LoadDefaultSettings()
    {
      Transport = new Transport.Loopback();
      //Transport = new Transport.SerialPortTransport();
      Modem = new Modem.NoModem();
      Terminal = new Terminal.TerminalTTY();
    }

    private void SetEvents()
    {
      if (Modem != null && Modem.Transport != Transport) Modem.Transport = Transport;
      if (Terminal != null && Terminal.Modem != Modem) Terminal.Modem = Modem;
    }

    public void SelectTransport(string Name)
    {
      if (Name != this.Transport.GetType().Name)
        this.Transport = ProviderInstanceControl.GetProviderInstance<ITransport>(Name) as ITransport;
      if (Transport == null)
        Transport = new Loopback();
    }

    public void SelectModem(string Name)
    {
      if (Name != this.Modem.GetType().Name)
        this.Modem = ProviderInstanceControl.GetProviderInstance<IModem>(Name) as IModem;
      if (Modem == null)
        Modem = new NoModem();
    }

    public void SelectTerminal(string Name)
    {
      if (Name != this.Terminal.GetType().Name)
        this.Terminal = ProviderInstanceControl.GetProviderInstance<ITerminal>(Name) as ITerminal;
      if (Terminal == null)
        Terminal = new TerminalTTY();
    }

    public List<string> GetProviderNames<T>() where T : class, ICommProvider
    {
      return Common.ProviderInstanceControl.GetProviderNames<T>();
    }

    public void Save(INIFile ConnectionFile)
    {
      ConnectionFile.SetValue("Transport", "Name", Transport.GetType().Name);
      Transport.ConfigData.Save(ConnectionFile, "Transport");

      ConnectionFile.SetValue("Terminal", "Name", Terminal.GetType().Name);
			Terminal.ConfigData.Save(ConnectionFile, "Terminal");

      ConnectionFile.SetValue("Modem", "Name", Modem.GetType().Name);
			Modem.ConfigData.Save(ConnectionFile, "Modem");
    }

    public void Load(INIFile ConnectionFile)
    {
      string name = ConnectionFile.GetValue("Transport", "Name");
      SelectTransport(name);
      Transport.ConfigData.Load(ConnectionFile, "Transport");

			name = ConnectionFile.GetValue("Modem", "Name");
      SelectModem(name);
      Modem.ConfigData.Load(ConnectionFile, "Modem");

			name = ConnectionFile.GetValue("Terminal", "Name");
      SelectTerminal(name);
      Terminal.ConfigData.Load(ConnectionFile, "Terminal");

      SetEvents();
    }

    string ExtractName(string ConfigString)
    {
      string[] parts = ConfigString.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
      foreach (string part in parts)
      {
        if (part.StartsWith("Type"))
        {
          string[] line = part.Split('=');
          if (line.Length > 1)
            return line[1];
        }
      }
      return "";
    }


    public void Disconnect()
    {
      if (Modem != null)
        Modem.Disconnect();
      if (Transport != null)
        Transport.Close();
    }

    public void Connect()
    {
      Modem.Connect();
    }

    public bool Connected
    {
      get
      {
        return Modem.Connected;
      }
    }

  }
}
