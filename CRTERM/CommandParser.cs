using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRTERM
{
	class CommandParser
	{
		string fileName = "";
		public void DispatchCommandLine(string[] args) {
			foreach (string item in args)
			{
				if (item.StartsWith("/"))
					ParseCommand(item);
				else
					fileName = item;
			}
			Application.Run(new UI.TerminalWindow(fileName));
		}

		private void ParseCommand(string item)
		{
			throw new NotImplementedException();
		}
	}
}
