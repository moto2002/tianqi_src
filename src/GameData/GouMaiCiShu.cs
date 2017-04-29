using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GouMaiCiShu")]
	[Serializable]
	public class GouMaiCiShu : IExtensible
	{
		private int _buyTimes;

		private int _moneyType;

		private int _cost;

		private readonly List<int> _burstProbability = new List<int>();

		private readonly List<int> _burstMultiple = new List<int>();

		private int _probability;

		private int _multiple2;

		private int _multiple3;

		private int _multiple4;

		private int _multiple5;

		private int _multiple6;

		private int _multiple7;

		private int _multiple8;

		private int _multiple9;

		private int _multiple10;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "buyTimes", DataFormat = DataFormat.TwosComplement)]
		public int buyTimes
		{
			get
			{
				return this._buyTimes;
			}
			set
			{
				this._buyTimes = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "moneyType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int moneyType
		{
			get
			{
				return this._moneyType;
			}
			set
			{
				this._moneyType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "cost", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cost
		{
			get
			{
				return this._cost;
			}
			set
			{
				this._cost = value;
			}
		}

		[ProtoMember(5, Name = "burstProbability", DataFormat = DataFormat.TwosComplement)]
		public List<int> burstProbability
		{
			get
			{
				return this._burstProbability;
			}
		}

		[ProtoMember(6, Name = "burstMultiple", DataFormat = DataFormat.TwosComplement)]
		public List<int> burstMultiple
		{
			get
			{
				return this._burstMultiple;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "multiple2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiple2
		{
			get
			{
				return this._multiple2;
			}
			set
			{
				this._multiple2 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "multiple3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiple3
		{
			get
			{
				return this._multiple3;
			}
			set
			{
				this._multiple3 = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "multiple4", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiple4
		{
			get
			{
				return this._multiple4;
			}
			set
			{
				this._multiple4 = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "multiple5", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiple5
		{
			get
			{
				return this._multiple5;
			}
			set
			{
				this._multiple5 = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "multiple6", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiple6
		{
			get
			{
				return this._multiple6;
			}
			set
			{
				this._multiple6 = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "multiple7", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiple7
		{
			get
			{
				return this._multiple7;
			}
			set
			{
				this._multiple7 = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "multiple8", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiple8
		{
			get
			{
				return this._multiple8;
			}
			set
			{
				this._multiple8 = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "multiple9", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiple9
		{
			get
			{
				return this._multiple9;
			}
			set
			{
				this._multiple9 = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "multiple10", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiple10
		{
			get
			{
				return this._multiple10;
			}
			set
			{
				this._multiple10 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
