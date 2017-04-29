using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"lv"
	}), ProtoContract(Name = "PetAttr")]
	[Serializable]
	public class PetAttr : IExtensible
	{
		private uint _lv;

		private uint _hp;

		private uint _att;

		private uint _defense;

		private uint _crt;

		private uint _res;

		private uint _hit;

		private uint _dex;

		private uint _spirit;

		private uint _relative;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public uint lv
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

		[ProtoMember(2, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this._hp = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "att", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint att
		{
			get
			{
				return this._att;
			}
			set
			{
				this._att = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "defense", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint defense
		{
			get
			{
				return this._defense;
			}
			set
			{
				this._defense = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "crt", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint crt
		{
			get
			{
				return this._crt;
			}
			set
			{
				this._crt = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "res", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint res
		{
			get
			{
				return this._res;
			}
			set
			{
				this._res = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "hit", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint hit
		{
			get
			{
				return this._hit;
			}
			set
			{
				this._hit = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "dex", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint dex
		{
			get
			{
				return this._dex;
			}
			set
			{
				this._dex = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "spirit", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint spirit
		{
			get
			{
				return this._spirit;
			}
			set
			{
				this._spirit = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "relative", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint relative
		{
			get
			{
				return this._relative;
			}
			set
			{
				this._relative = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
