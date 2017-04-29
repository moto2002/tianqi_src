using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1028), ForSend(1028), ProtoContract(Name = "ExitFromGangFightFieldReq")]
	[Serializable]
	public class ExitFromGangFightFieldReq : IExtensible
	{
		public static readonly short OP = 1028;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
