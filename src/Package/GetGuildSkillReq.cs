using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(173), ForSend(173), ProtoContract(Name = "GetGuildSkillReq")]
	[Serializable]
	public class GetGuildSkillReq : IExtensible
	{
		public static readonly short OP = 173;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
