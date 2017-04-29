using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "DungeonStarLv")]
	[Serializable]
	public class DungeonStarLv : IExtensible
	{
		private int _id;

		private int _complete;

		private int _suvival;

		private int _hp1;

		private int _hp2;

		private int _time;

		private int _num1;

		private int _num2;

		private int _element;

		private int _time2;

		private int _sameElementPetNum2;

		private int _sameElementPetNum3;

		private int _introduction;

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

		[ProtoMember(2, IsRequired = false, Name = "complete", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int complete
		{
			get
			{
				return this._complete;
			}
			set
			{
				this._complete = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "suvival", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suvival
		{
			get
			{
				return this._suvival;
			}
			set
			{
				this._suvival = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "hp1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hp1
		{
			get
			{
				return this._hp1;
			}
			set
			{
				this._hp1 = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "hp2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hp2
		{
			get
			{
				return this._hp2;
			}
			set
			{
				this._hp2 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "num1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num1
		{
			get
			{
				return this._num1;
			}
			set
			{
				this._num1 = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "num2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num2
		{
			get
			{
				return this._num2;
			}
			set
			{
				this._num2 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "element", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int element
		{
			get
			{
				return this._element;
			}
			set
			{
				this._element = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "time2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time2
		{
			get
			{
				return this._time2;
			}
			set
			{
				this._time2 = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "sameElementPetNum2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sameElementPetNum2
		{
			get
			{
				return this._sameElementPetNum2;
			}
			set
			{
				this._sameElementPetNum2 = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "sameElementPetNum3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sameElementPetNum3
		{
			get
			{
				return this._sameElementPetNum3;
			}
			set
			{
				this._sameElementPetNum3 = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "introduction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int introduction
		{
			get
			{
				return this._introduction;
			}
			set
			{
				this._introduction = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
