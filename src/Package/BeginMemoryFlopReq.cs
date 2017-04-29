using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2000), ForSend(2000), ProtoContract(Name = "BeginMemoryFlopReq")]
	[Serializable]
	public class BeginMemoryFlopReq : IExtensible
	{
		public static readonly short OP = 2000;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
