using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(164), ForSend(164), ProtoContract(Name = "MultiPvpReliveReq")]
	[Serializable]
	public class MultiPvpReliveReq : IExtensible
	{
		public static readonly short OP = 164;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
