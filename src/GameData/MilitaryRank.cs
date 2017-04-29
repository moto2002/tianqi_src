using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "MilitaryRank")]
	[Serializable]
	public class MilitaryRank : IExtensible
	{
		private int _id;

		private int _lv;

		private int _reward;

		private int _icon;

		private int _prestige;

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "reward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reward
		{
			get
			{
				return this._reward;
			}
			set
			{
				this._reward = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "prestige", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int prestige
		{
			get
			{
				return this._prestige;
			}
			set
			{
				this._prestige = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
