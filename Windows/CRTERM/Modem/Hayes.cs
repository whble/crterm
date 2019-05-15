using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTERM.Transport;

namespace CRTERM.Modem
{
	class HayesModem : NoModem
	{
		public HayesModem()
		{
			ConfigData.Set("Number", "619-555-1212");
		}
	}
}
