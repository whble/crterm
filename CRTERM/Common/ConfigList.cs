using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTERM.Common
{
	public class ConfigList : SortedList<string, ConfigItem>
	{
		public ConfigList()
		{
		}
		public void Set(string Label, string Value)
		{
			ConfigItem ci = new ConfigItem(Label, Value);
			Set(Label, ci);
		}
		public void Set(string Label, int Value)
		{
			ConfigItem ci = new ConfigItem(Label, Value);
			Set(Label, ci);
		}
		public void Set(string Label, bool Value)
		{
			ConfigItem ci = new ConfigItem(Label, Value);
			Set(Label, ci);
		}
		public void Set(string Label, string Value, string[] PickList)
		{
			ConfigItem ci = new ConfigItem(Label, Value, PickList);
			Set(Label, ci);
		}

		protected void Set(string Label, ConfigItem Value)
		{
			if (this.ContainsKey(Label))
				this[Label] = Value;
			else
				this.Add(Label,Value);
		}

		public void Load(INIFile ConnectionFile, string BlockName)
		{
			foreach (string key in ConnectionFile.Blocks[BlockName].Keys)
			{
				if (this.ContainsKey(key))
					this[key].Value = ConnectionFile.Blocks[BlockName][key];
				else
					this.Set(key, ConnectionFile.Blocks[BlockName][key]);
			}
		}

		public void Save(INIFile ConnectionFile, string BlockName)
		{
			foreach (ConfigItem item in this.Values)
			{
				ConnectionFile.SetValue(BlockName,item.Name,item.Value);
			}
		}
	}
}
