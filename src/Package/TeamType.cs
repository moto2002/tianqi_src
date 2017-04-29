using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "TeamType")]
	[Serializable]
	public class TeamType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "TEAM", Value = 1)]
			TEAM = 1,
			[ProtoEnum(Name = "DEFEND", Value = 2)]
			DEFEND
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
