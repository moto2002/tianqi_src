using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildId2Name")]
	[Serializable]
	public class GuildId2Name : IExtensible
	{
		private long _guildId;

		private string _guildName;

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

		[ProtoMember(2, IsRequired = true, Name = "guildName", DataFormat = DataFormat.Default)]
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this._guildName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
