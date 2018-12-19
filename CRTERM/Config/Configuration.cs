using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CRTerm.Terminals;

namespace CRTerm.Config
{
    public class Configuration
    {
        const string AppDataFolder = "CRTerm";
        const string ConfigFile = "CRTerm.ini";

        public string SectionName = "";

        public List<IConfigurable> ConfigurableObjects = new List<IConfigurable>();
        public Dictionary<string, string> ConfigData = new Dictionary<string, string>();

        public void Set(string Key, object Value)
        {
            if (!ConfigData.ContainsKey(Key))
                ConfigData.Add(Key, Value?.ToString());
            else
                ConfigData[Key] = Value?.ToString();
        }

        public T Get<T>(string Key)
        {
            if (!ConfigData.ContainsKey(Key))
                return default(T);
            return (T)Convert.ChangeType(ConfigData[Key], typeof(T));
        }

        public string GetConfigurationPath()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dir = Path.Combine(dir, AppDataFolder);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, ConfigFile);
        }

        public void SaveConfiguration()
        {
            foreach (object item in ConfigurableObjects)
            {
                if (item == null)
                    continue;

                ReadProperties(item);
            }
            WriteConfigFile();
        }

        private void ReadProperties(object obj)
        {
            string typeName = obj.GetType().Name;

            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                //if (p.PropertyType.Name == "String"
                //    || p.PropertyType.Name == "Int32")
                if (p.GetCustomAttributes(typeof(ConfigItem), true).Length > 0)
                    Set(typeName + "." + p.Name, p.GetValue(obj, null)?.ToString());
            }
        }

        private void WriteConfigFile()
        {
            StringBuilder s = new StringBuilder();
            foreach (string key in ConfigData.Keys)
            {
                object value = ConfigData[key];
                s.Append(key);
                s.Append("=");
                if (value == null)
                    s.Append("");
                else
                    s.Append(value.ToString());
                s.AppendLine();
            }

            string fileName = GetConfigurationPath();
            System.IO.File.WriteAllText(fileName, s.ToString());
        }

        public void LoadConfiguration(Session session)
        {
            LoadConfiguration();

            CreatePort(session);
            CreateTerminal(session);
        }

        private void CreateTerminal(Session session)
        {
            ITerminal term;
            switch (Get<string>("Session.Terminal"))
            {
                case "CRTerm.Terminals.ANSITerminal":
                    term = new Terminals.ANSITerminal();
                    PopulateObject(term);
                    session.Terminal = term;
                    break;
                default:
                    term = new Terminals.ANSITerminal();
                    PopulateObject(term);
                    session.Terminal = term;
                    break;

            }
        }

        private void CreatePort(Session session)
        {
            switch (Get<string>("Session.Transport"))
            {
                case "CRTerm.IO.SerialIOPort":
                    IO.SerialIOPort port = new IO.SerialIOPort();
                    PopulateObject(port);
                    session.Transport = port;
                    break;
                case "CRTerm.IO.TestPort":
                case "":
                    IO.TestPort testPort = new IO.TestPort();
                    PopulateObject(testPort);
                    session.Transport = testPort;
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("Session not implemented: " + Get<string>("Session.Transport"));
                    IO.TestPort defaultPort = new IO.TestPort();
                    PopulateObject(defaultPort);
                    session.Transport = defaultPort;
                    break;
            }
        }

        private void PopulateObject(IConfigurable obj)
        {

            string key;
            string typeName = obj.GetType().Name;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                //if (p.PropertyType.Name == "String"
                //    || p.PropertyType.Name == "Int32")
                if (p.GetCustomAttributes(typeof(ConfigItem), true).Length > 0)
                {
                    key = typeName + "." + p.Name;
                    if (!ConfigData.ContainsKey(key))
                        continue;

                    switch (p.PropertyType.Name)
                    {
                        case "String":
                            p.SetValue(obj, Get<String>(key), null);
                            break;
                        case "Int32":
                            p.SetValue(obj, Get<int>(key), null);
                            break;
                        case "Boolean":
                            p.SetValue(obj, Get<bool>(key), null);
                            break;
                    }
                    //if (p.PropertyType.Name == "System.String")
                    //    p.SetValue(obj, Get<string>(key), null);
                    //Set(typeName + "." + p.Name, p.GetValue(obj, null)?.ToString());
                }
            }

        }

        private void LoadConfiguration()
        {
            string filename = GetConfigurationPath();
            if (!System.IO.File.Exists(filename))
                return;

            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string key = "", value = "";
                string[] parts = line.Split(new char[] { '=', ':' });
                if (parts.Length > 0)
                    key = parts[0];
                if (parts.Length > 1)
                    value = parts[1];

                if (key != "")
                    Set(key, value);
            }

        }

    }
}
