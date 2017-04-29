using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7623), ForSend(7623), ProtoContract(Name = "BuySkillConfigRes")]
	[Serializable]
	public class BuySkillConfigRes : IExtensible
	{
		public static readonly short OP = 7623;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
