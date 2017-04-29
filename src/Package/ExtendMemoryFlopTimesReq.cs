using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2005), ForSend(2005), ProtoContract(Name = "ExtendMemoryFlopTimesReq")]
	[Serializable]
	public class ExtendMemoryFlopTimesReq : IExtensible
	{
		public static readonly short OP = 2005;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
