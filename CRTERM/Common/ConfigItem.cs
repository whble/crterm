using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CRTERM.Common
{
	[Serializable]
	public class ConfigItem
	{
		public enum ConfigItemTypes
		{
			Text = 0,
			List,
			YesNo
		}

		// Member variables
		public ConfigItemTypes ItemType = ConfigItemTypes.Text;
		private string _name = "";
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
		private string _value = "";
		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}
		public string[] PickList = null;

		// Constructors
		public ConfigItem() { }
		public ConfigItem(string Label)
		{
			this._value = Label;
		}

		public ConfigItem(ConfigItemTypes ItemType, string Name, string CurrentValue)
		{
			this.ItemType = ItemType;
			this._name = Name;
			this._value = CurrentValue;
		}

		public ConfigItem(string Name, bool CurrentValue)
			: this(ConfigItemTypes.YesNo, Name, CurrentValue.ToString())
		{
		}

		public ConfigItem(string Name, int CurrentValue)
			: this(ConfigItemTypes.Text, Name, CurrentValue.ToString())
		{
		}

		public ConfigItem(string Name, string CurrentValue)
			: this(ConfigItemTypes.Text, Name, CurrentValue)
		{
		}

		public ConfigItem(string Name, string CurrentValue, string[] PickList) :
			this(ConfigItemTypes.List, Name, CurrentValue)
		{
			this.PickList = PickList;
		}

		public void Parse(string Config)
		{
			string[] parts = Config.Split('=');
			if (parts.Length != 2)
				throw new TerminalException("Invalid Config Sequence.\r\nRequired: \"Name=Value\"");
			this.Name = parts[0];
			this.Value = parts[1];
		}

		public bool BoolValue
		{
			get
			{
				bool ret = false;
				bool.TryParse(this.Value, out ret);
				return ret;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		public int IntValue
		{
			get
			{
				int ret = 0;
				int.TryParse(this.Value, out ret);
				return ret;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		public override string ToString()
		{
			return this.Value;
		}

	}
}
