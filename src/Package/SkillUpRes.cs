using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1782), ForSend(1782), ProtoContract(Name = "SkillUpRes")]
	[Serializable]
	public class SkillUpRes : IExtensible
	{
		public static readonly short OP = 1782;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
