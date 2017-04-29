using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4521), ForSend(4521), ProtoContract(Name = "SetSkillFormationRes")]
	[Serializable]
	public class SetSkillFormationRes : IExtensible
	{
		public static readonly short OP = 4521;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
