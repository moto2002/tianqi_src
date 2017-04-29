using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "VipDengJi")]
	[Serializable]
	public class VipDengJi : IExtensible
	{
		private int _level;

		private int _icon;

		private int _icon2;

		private int _diamonds;

		private int _vipExp;

		private readonly List<int> _effect = new List<int>();

		private int _titleId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "level", DataFormat = DataFormat.TwosComplement)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "icon2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon2
		{
			get
			{
				return this._icon2;
			}
			set
			{
				this._icon2 = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "diamonds", DataFormat = DataFormat.TwosComplement)]
		public int diamonds
		{
			get
			{
				return this._diamonds;
			}
			set
			{
				this._diamonds = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "vipExp", DataFormat = DataFormat.TwosComplement)]
		public int vipExp
		{
			get
			{
				return this._vipExp;
			}
			set
			{
				this._vipExp = value;
			}
		}

		[ProtoMember(7, Name = "effect", DataFormat = DataFormat.TwosComplement)]
		public List<int> effect
		{
			get
			{
				return this._effect;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "titleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int titleId
		{
			get
			{
				return this._titleId;
			}
			set
			{
				this._titleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
