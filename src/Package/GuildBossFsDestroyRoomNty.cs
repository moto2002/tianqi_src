using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildBossFsDestroyRoomNty")]
	[Serializable]
	public class GuildBossFsDestroyRoomNty : IExtensible
	{
		private long _guildId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "guildId", DataFormat = DataFormat.TwosComplement)]
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this._guildId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
