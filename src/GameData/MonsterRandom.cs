using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "MonsterRandom")]
	[Serializable]
	public class MonsterRandom : IExtensible
	{
		private int _id;

		private readonly List<int> _librariesId = new List<int>();

		private readonly List<int> _randomTypeNum = new List<int>();

		private readonly List<int> _num = new List<int>();

		private int _serialNum;

		private int _inteval;

		private readonly List<int> _bornPoint = new List<int>();

		private int _appointedId;

		private int _appointedNum;

		private int _appointedDelay;

		private readonly List<int> _appointedNumRange = new List<int>();

		private int _appointedInterval;

		private int _bornTips;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, Name = "librariesId", DataFormat = DataFormat.TwosComplement)]
		public List<int> librariesId
		{
			get
			{
				return this._librariesId;
			}
		}

		[ProtoMember(4, Name = "randomTypeNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> randomTypeNum
		{
			get
			{
				return this._randomTypeNum;
			}
		}

		[ProtoMember(5, Name = "num", DataFormat = DataFormat.TwosComplement)]
		public List<int> num
		{
			get
			{
				return this._num;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "serialNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int serialNum
		{
			get
			{
				return this._serialNum;
			}
			set
			{
				this._serialNum = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "inteval", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int inteval
		{
			get
			{
				return this._inteval;
			}
			set
			{
				this._inteval = value;
			}
		}

		[ProtoMember(8, Name = "bornPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> bornPoint
		{
			get
			{
				return this._bornPoint;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "appointedId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int appointedId
		{
			get
			{
				return this._appointedId;
			}
			set
			{
				this._appointedId = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "appointedNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int appointedNum
		{
			get
			{
				return this._appointedNum;
			}
			set
			{
				this._appointedNum = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "appointedDelay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int appointedDelay
		{
			get
			{
				return this._appointedDelay;
			}
			set
			{
				this._appointedDelay = value;
			}
		}

		[ProtoMember(12, Name = "appointedNumRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> appointedNumRange
		{
			get
			{
				return this._appointedNumRange;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "appointedInterval", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int appointedInterval
		{
			get
			{
				return this._appointedInterval;
			}
			set
			{
				this._appointedInterval = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "bornTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bornTips
		{
			get
			{
				return this._bornTips;
			}
			set
			{
				this._bornTips = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
