using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "TeamMatchStatus")]
	[Serializable]
	public class TeamMatchStatus : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "AutoMatch", Value = 1)]
			AutoMatch = 1,
			[ProtoEnum(Name = "QuickEnter", Value = 2)]
			QuickEnter,
			[ProtoEnum(Name = "LeaderInvite", Value = 3)]
			LeaderInvite
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
