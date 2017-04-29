using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1292), ForSend(1292), ProtoContract(Name = "SkillConfigRes")]
	[Serializable]
	public class SkillConfigRes : IExtensible
	{
		public static readonly short OP = 1292;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
