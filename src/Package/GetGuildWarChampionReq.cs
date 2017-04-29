using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2182), ForSend(2182), ProtoContract(Name = "GetGuildWarChampionReq")]
	[Serializable]
	public class GetGuildWarChampionReq : IExtensible
	{
		public static readonly short OP = 2182;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
