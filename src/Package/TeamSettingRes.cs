using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4054), ForSend(4054), ProtoContract(Name = "TeamSettingRes")]
	[Serializable]
	public class TeamSettingRes : IExtensible
	{
		public static readonly short OP = 4054;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
