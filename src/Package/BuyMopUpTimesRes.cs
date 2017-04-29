using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3214), ForSend(3214), ProtoContract(Name = "BuyMopUpTimesRes")]
	[Serializable]
	public class BuyMopUpTimesRes : IExtensible
	{
		public static readonly short OP = 3214;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
