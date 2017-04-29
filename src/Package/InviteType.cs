using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "InviteType")]
	[Serializable]
	public class InviteType : IExtensible
	{
		[ProtoContract(Name = "IMT")]
		public enum IMT
		{
			[ProtoEnum(Name = "OtherInvite", Value = 0)]
			OtherInvite,
			[ProtoEnum(Name = "OtherAgree", Value = 1)]
			OtherAgree
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
