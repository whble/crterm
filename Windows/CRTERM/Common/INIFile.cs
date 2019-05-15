using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTERM.Common
{
	public class INIFile
	{
		public class INIBlock : SortedList<string, string>
		{
			private string _name = "";

			public string Name
			{
				get { return _name; }
				set
				{
					_name = value.Trim(new char[] { '[', ']' });
				}
			}
			public void SetValue(string KeyValue)
			{
				int pos = KeyValue.IndexOf('=');
				if (pos <= 0 || pos >= KeyValue.Length)
					return;
				SetValue(KeyValue.Substring(0, pos), KeyValue.Substring(pos + 1));
			}

			public void SetValue(string Key, string Value)
			{
				if (this.ContainsKey(Key))
					this[Key] = Value;
				else
					this.Add(Key, Value);
			}

		}

		public INIFile()
		{
		}

		public INIFile(string FileName)
		{
			Load(FileName);
		}

		public void Load(string Path)
		{
			string[] lines = System.IO.File.ReadAllLines(Path);
			ParseText(lines);
		}

		private void ParseText(string[] lines)
		{
			INIBlock block = new INIBlock();
			block.Name = "";
			foreach (string lineCR in lines)
			{
				string line = lineCR.TrimEnd('\r');
				if (line.StartsWith("["))
				{
					block = new INIBlock();
					block.Name = line;
					if (!Blocks.ContainsKey(block.Name))
						Blocks.Add(block.Name, block);
					else
						block = Blocks[block.Name];
				}
				else if (! line.StartsWith(";") && !line.StartsWith("#") && line.Contains("="))
				{
					block.SetValue(line);
				}
			}
		}

		public void Save(string Path)
		{
			System.IO.File.WriteAllText(Path, this.Text);
		}

		public string Text
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				foreach (INIBlock block in Blocks.Values)
				{
					sb.AppendLine("[" + Escape(block.Name) + "]");
					foreach (var blockKey in block.Keys)
					{
						sb.AppendLine(Escape(blockKey) + "=" + Escape(block[blockKey]));
					}
				}
				return sb.ToString();
			}
			set
			{
				string[] lines = value.Split('\n');
				ParseText(lines);
			}
		}

		public SortedList<string, INIBlock> Blocks = new SortedList<string, INIBlock>();

		public static string Escape(string text)
		{
			text = text.Replace("\"", "\\\"");
			text = text.Replace("\\", "\\\\");
			return text;
		}

		public static string UnEscape(string text)
		{
			text = text.Replace("\\\"", "\"");
			text = text.Replace("\\\\", "\\");
			return text;
		}

		public void SetValue(string BlockName, string Key, string Value)
		{
			if (!Blocks.ContainsKey(BlockName))
			{
				INIBlock block = new INIBlock();
				block.Name = BlockName;
				Blocks.Add(block.Name, block);
			}
			Blocks[BlockName].SetValue(Key, Value);
		}

		public string GetValue(string BlockName, string ItemName)
		{
			if (Blocks.ContainsKey(BlockName) && Blocks[BlockName].ContainsKey(ItemName))
				return Blocks[BlockName][ItemName];

			return "";
		}

		public SortedList<string,string> GetValues(string BlockName)
		{
			if (Blocks.ContainsKey(BlockName))
				return Blocks[BlockName];

			return new INIBlock();
		}

	}
}
