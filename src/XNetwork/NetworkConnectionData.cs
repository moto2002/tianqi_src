using System;
using System.Net.Sockets;

namespace XNetwork
{
	public class NetworkConnectionData
	{
		public const int UnavailableID = 0;

		protected static int poolID;

		protected int id;

		protected Socket connection;

		protected static int PoolID
		{
			get
			{
				NetworkConnectionData.poolID++;
				return NetworkConnectionData.poolID;
			}
		}

		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public Socket Connection
		{
			get
			{
				return this.connection;
			}
		}

		public NetworkConnectionData(Socket theConnection)
		{
			this.id = NetworkConnectionData.PoolID;
			this.connection = theConnection;
		}

		public void Release()
		{
			if (this.connection == null)
			{
				return;
			}
			if (this.connection.get_Connected())
			{
				this.connection.Shutdown(2);
			}
			this.connection.Close();
			this.connection = null;
		}
	}
}
