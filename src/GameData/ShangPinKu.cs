using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShangPinKu")]
	[Serializable]
	public class ShangPinKu : IExtensible
	{
		private int _commodityPool;

		private int _commodityId;

		private int _weight;

		private int _specialDisplay;

		private int _vipLvLimit;

		private readonly List<int> _startTime = new List<int>();

		private readonly List<int> _endTime = new List<int>();

		private int _startLv;

		private int _endLv;

		private int _job;

		private int _startPvpLevel;

		private int _closePvpLevel;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "commodityPool", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int commodityPool
		{
			get
			{
				return this._commodityPool;
			}
			set
			{
				this._commodityPool = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "commodityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int commodityId
		{
			get
			{
				return this._commodityId;
			}
			set
			{
				this._commodityId = value;
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

		[ProtoMember(5, IsRequired = false, Name = "specialDisplay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int specialDisplay
		{
			get
			{
				return this._specialDisplay;
			}
			set
			{
				this._specialDisplay = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "vipLvLimit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vipLvLimit
		{
			get
			{
				return this._vipLvLimit;
			}
			set
			{
				this._vipLvLimit = value;
			}
		}

		[ProtoMember(7, Name = "startTime", DataFormat = DataFormat.TwosComplement)]
		public List<int> startTime
		{
			get
			{
				return this._startTime;
			}
		}

		[ProtoMember(8, Name = "endTime", DataFormat = DataFormat.TwosComplement)]
		public List<int> endTime
		{
			get
			{
				return this._endTime;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "startLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int startLv
		{
			get
			{
				return this._startLv;
			}
			set
			{
				this._startLv = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "endLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int endLv
		{
			get
			{
				return this._endLv;
			}
			set
			{
				this._endLv = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "job", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int job
		{
			get
			{
				return this._job;
			}
			set
			{
				this._job = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "startPvpLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int startPvpLevel
		{
			get
			{
				return this._startPvpLevel;
			}
			set
			{
				this._startPvpLevel = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "closePvpLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int closePvpLevel
		{
			get
			{
				return this._closePvpLevel;
			}
			set
			{
				this._closePvpLevel = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
