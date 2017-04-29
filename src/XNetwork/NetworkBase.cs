using System;

namespace XNetwork
{
	public class NetworkBase
	{
		protected ServerType serverType;

		public ServerType ServerType
		{
			get
			{
				return this.serverType;
			}
			protected set
			{
				this.serverType = value;
			}
		}

		public NetworkBase()
		{
		}

		public NetworkBase(ServerType theServerType)
		{
			this.ServerType = theServerType;
		}
	}
}
