using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2102), ForSend(2102), ProtoContract(Name = "DarkTrainQuickEnterReq")]
	[Serializable]
	public class DarkTrainQuickEnterReq : IExtensible
	{
		public static readonly short OP = 2102;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
