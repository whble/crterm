using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CRTERM.Transport
{
	public enum ConfigItemTypes
	{
		Label,
		Text,
		List,
		YesNo
	}

	public class TransportConfigItem
	{
		// Member variables
		public ConfigItemTypes ItemType = ConfigItemTypes.Text;
		public string Name = "";
		public string Value = "";
		public List<string> ListItems = new List<string>();

		// Constructors
		public TransportConfigItem() { }
		public TransportConfigItem(string Label) {
			this.Value = Label;
		}
		public TransportConfigItem(ConfigItemTypes ItemType, string Name, string CurrentValue)
		{
			this.ItemType = ItemType;
			this.Name = Name;
			this.Value = CurrentValue;
		}
		public TransportConfigItem(ConfigItemTypes ItemType, string Name, string CurrentValue, List<string> ListItems) :
			this(ItemType, Name, CurrentValue)
		{
			this.ListItems = ListItems;
		}

	}

	public class TransportItem
	{
		public string Name;
		public string AssemblyName;
		public string TypeName;
	}

	public class TransportException : Exception
	{
		public TransportException(string Message) : base(Message) { }
		public TransportException(string Message, Exception InnerException) : base(Message, InnerException) { }
	}

	public class TransportManager
	{
		private SortedList<string, TransportItem> TransportList = new SortedList<string, TransportItem>();

		public TransportManager()
		{
			populateTransportList();
		}

		private void populateTransportList()
		{
			TransportList.Clear();
			Assembly thisAsm = Assembly.GetExecutingAssembly();
			List<Type> types = thisAsm.GetTypes().Where(
				t => ((typeof(ITransport).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract))).ToList();
			foreach (Type tranType in types)
			{
				ITransport transport = (ITransport)Activator.CreateInstance(tranType);
				TransportItem item = new TransportItem();
				item.Name = transport.Name;
				item.AssemblyName = tranType.Assembly.FullName;
				item.TypeName = tranType.FullName;
				TransportList.Add(transport.Name, item);
			}
		}

		public List<string> GetTransportProviders()
		{
			return (List<string>)TransportList.Keys;
		}

		public ITransport GetTransport(string TransportName)
		{
			if (TransportName == "")
				return new TransportBase();
			if (TransportList.ContainsKey(TransportName))
			{
				TransportItem ti=TransportList[TransportName];
				ITransport transport = (ITransport)Activator.CreateInstance(ti.AssemblyName, ti.TypeName);
				return transport;
			}
			throw new TransportException("Could not retrieve transport: " + TransportName + "\r\n" +
				"Use GetTransportProviders() to get a list of valid transport names.");
		}

	}
}
