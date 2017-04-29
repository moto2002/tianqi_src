using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(191), ForSend(191), ProtoContract(Name = "GuildAttrNty")]
	[Serializable]
	public class GuildAttrNty : IExtensible
	{
		[ProtoContract(Name = "AttrType")]
		public enum AttrType
		{
			[ProtoEnum(Name = "contribute", Value = 1)]
			contribute = 1,
			[ProtoEnum(Name = "equipEssence", Value = 2)]
			equipEssence,
			[ProtoEnum(Name = "activity", Value = 3)]
			activity,
			[ProtoEnum(Name = "guildFund", Value = 4)]
			guildFund,
			[ProtoEnum(Name = "guildLv", Value = 5)]
			guildLv
		}

		public static readonly short OP = 191;

		private GuildAttrNty.AttrType _attrType;

		private long _value;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "attrType", DataFormat = DataFormat.TwosComplement)]
		public GuildAttrNty.AttrType attrType
		{
			get
			{
				return this._attrType;
			}
			set
			{
				this._attrType = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "value", DataFormat = DataFormat.TwosComplement)]
		public long value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
