using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "DefendFightStep")]
	[Serializable]
	public class DefendFightStep : IExtensible
	{
		[ProtoContract(Name = "DFS")]
		public enum DFS
		{
			[ProtoEnum(Name = "AllRoleLoaded", Value = 1)]
			AllRoleLoaded = 1,
			[ProtoEnum(Name = "BossRandomEnter", Value = 2)]
			BossRandomEnter
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
