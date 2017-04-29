using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "RenWuQiangHuaShuXing")]
	[Serializable]
	public class RenWuQiangHuaShuXing : IExtensible
	{
		private int _id;

		private readonly List<int> _type = new List<int>();

		private readonly List<int> _nun = new List<int>();

		private int _minLv;

		private int _precondition;

		private readonly List<int> _way = new List<int>();

		private readonly List<int> _consume = new List<int>();

		private int _word;

		private int _happen;

		private int _basevalue;

		private int _highvalue;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public List<int> type
		{
			get
			{
				return this._type;
			}
		}

		[ProtoMember(4, Name = "nun", DataFormat = DataFormat.TwosComplement)]
		public List<int> nun
		{
			get
			{
				return this._nun;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "precondition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int precondition
		{
			get
			{
				return this._precondition;
			}
			set
			{
				this._precondition = value;
			}
		}

		[ProtoMember(7, Name = "way", DataFormat = DataFormat.TwosComplement)]
		public List<int> way
		{
			get
			{
				return this._way;
			}
		}

		[ProtoMember(8, Name = "consume", DataFormat = DataFormat.TwosComplement)]
		public List<int> consume
		{
			get
			{
				return this._consume;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "word", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int word
		{
			get
			{
				return this._word;
			}
			set
			{
				this._word = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "happen", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int happen
		{
			get
			{
				return this._happen;
			}
			set
			{
				this._happen = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "basevalue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int basevalue
		{
			get
			{
				return this._basevalue;
			}
			set
			{
				this._basevalue = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "highvalue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int highvalue
		{
			get
			{
				return this._highvalue;
			}
			set
			{
				this._highvalue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
