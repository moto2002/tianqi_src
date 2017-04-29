using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(432), ForSend(432), ProtoContract(Name = "PveBuildTeamReq")]
	[Serializable]
	public class PveBuildTeamReq : IExtensible
	{
		public static readonly short OP = 432;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
