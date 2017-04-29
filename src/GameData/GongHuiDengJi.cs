using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GongHuiDengJi")]
	[Serializable]
	public class GongHuiDengJi : IExtensible
	{
		private int _lv;

		private int _gold;

		private int _num;

		private readonly List<int> _function = new List<int>();

		private int _chairman;

		private int _vicechairman;

		private int _director;

		private int _member;

		private int _icon;

		private int _picture;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "gold", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gold
		{
			get
			{
				return this._gold;
			}
			set
			{
				this._gold = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(5, Name = "function", DataFormat = DataFormat.TwosComplement)]
		public List<int> function
		{
			get
			{
				return this._function;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "chairman", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chairman
		{
			get
			{
				return this._chairman;
			}
			set
			{
				this._chairman = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "vicechairman", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vicechairman
		{
			get
			{
				return this._vicechairman;
			}
			set
			{
				this._vicechairman = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "director", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int director
		{
			get
			{
				return this._director;
			}
			set
			{
				this._director = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "member", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int member
		{
			get
			{
				return this._member;
			}
			set
			{
				this._member = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "picture", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int picture
		{
			get
			{
				return this._picture;
			}
			set
			{
				this._picture = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
