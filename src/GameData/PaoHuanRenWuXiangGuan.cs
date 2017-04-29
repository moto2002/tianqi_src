using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "PaoHuanRenWuXiangGuan")]
	[Serializable]
	public class PaoHuanRenWuXiangGuan : IExtensible
	{
		private int _type;

		private int _lv;

		private int _weight;

		private int _lvModulus;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "lvModulus", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lvModulus
		{
			get
			{
				return this._lvModulus;
			}
			set
			{
				this._lvModulus = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
