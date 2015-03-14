using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CRTERM
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			CommandParser parser = new CommandParser();
			parser.DispatchCommandLine(args);
			//Application.Run(new UI.FontTestPad());
		}

	}
}
