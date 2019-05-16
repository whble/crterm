using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTERM
{
	class ConnectionLibrary
	{
		public Connection GetLastConnection()
		{
			return GetNewConnection();
		}

		public void SaveConnections(string FileName)
		{
		}

		public void LoadConnections(string FileName)
		{
		}

		public Connection GetNewConnection()
		{
			Connection c = new Connection();
			c.Load();
			return c;
		}

		public Connection GetConnection(string ConnectionName)
		{
			return GetNewConnection();
		}

		public void SaveConnection()
		{
		}
	}
}
