using System;

namespace XNetwork
{
	public delegate void NetCallBackMethod<T>(short state, T down = null) where T : class;
}
