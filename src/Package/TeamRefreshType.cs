using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "TeamRefreshType")]
	[Serializable]
	public class TeamRefreshType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Enter", Value = 1)]
			Enter = 1,
			[ProtoEnum(Name = "Leave", Value = 2)]
			Leave,
			[ProtoEnum(Name = "Update", Value = 3)]
			Update
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
