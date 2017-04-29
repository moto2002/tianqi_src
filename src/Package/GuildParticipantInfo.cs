using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildParticipantInfo")]
	[Serializable]
	public class GuildParticipantInfo : IExtensible
	{
		private long _guildId;

		private string _name;

		private int _lv;

		private long _fighting;

		private int _activity;

		private int _rank;

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

		[ProtoMember(2, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "fighting", DataFormat = DataFormat.TwosComplement)]
		public long fighting
		{
			get
			{
				return this._fighting;
			}
			set
			{
				this._fighting = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "activity", DataFormat = DataFormat.TwosComplement)]
		public int activity
		{
			get
			{
				return this._activity;
			}
			set
			{
				this._activity = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "rank", DataFormat = DataFormat.TwosComplement)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
