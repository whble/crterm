using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTERM.Modem
{
	class InitScript
	{
		private IModem modem = null;
		private List<string> script = new List<string>();

		public InitScript(Modem.IModem Modem)
		{
			this.modem = Modem;
		}

		public void Run(string InitScript)
		{
			script.Clear();
			foreach (string s in InitScript.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
			{
				string s1 = s.Replace("\r", "");
				script.Add(s1);
			}
		}
	}
}
