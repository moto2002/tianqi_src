using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "LeaveGuildReason")]
	[Serializable]
	public class LeaveGuildReason : IExtensible
	{
		[ProtoContract(Name = "LGDR")]
		public enum LGDR
		{
			[ProtoEnum(Name = "Chairman", Value = 1)]
			Chairman = 1,
			[ProtoEnum(Name = "System", Value = 2)]
			System,
			[ProtoEnum(Name = "RoleExit", Value = 3)]
			RoleExit,
			[ProtoEnum(Name = "KickOut", Value = 4)]
			KickOut
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
