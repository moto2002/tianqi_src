using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1213), ForSend(1213), ProtoContract(Name = "SkillUnblockRes")]
	[Serializable]
	public class SkillUnblockRes : IExtensible
	{
		public static readonly short OP = 1213;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
