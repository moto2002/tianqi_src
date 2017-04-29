using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "IntensifyInfo")]
	[Serializable]
	public class IntensifyInfo : IExtensible
	{
		private int _position;

		private int _lv;

		private int _blessValue;

		private int _blessRatio;

		private readonly List<long> _fightingList = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "blessValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int blessValue
		{
			get
			{
				return this._blessValue;
			}
			set
			{
				this._blessValue = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "blessRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int blessRatio
		{
			get
			{
				return this._blessRatio;
			}
			set
			{
				this._blessRatio = value;
			}
		}

		[ProtoMember(5, Name = "fightingList", DataFormat = DataFormat.TwosComplement)]
		public List<long> fightingList
		{
			get
			{
				return this._fightingList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
