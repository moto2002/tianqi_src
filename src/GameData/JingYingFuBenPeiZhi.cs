using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JingYingFuBenPeiZhi")]
	[Serializable]
	public class JingYingFuBenPeiZhi : IExtensible
	{
		[ProtoContract(Name = "NormalshowPair")]
		[Serializable]
		public class NormalshowPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		[ProtoContract(Name = "FirstbloodshowPair")]
		[Serializable]
		public class FirstbloodshowPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _id;

		private int _level;

		private int _map;

		private int _bossName;

		private int _bossintroduce;

		private int _bossPic;

		private int _rank;

		private int _step;

		private int _copyId;

		private int _power;

		private readonly List<int> _normalDrop = new List<int>();

		private readonly List<JingYingFuBenPeiZhi.NormalshowPair> _normalShow = new List<JingYingFuBenPeiZhi.NormalshowPair>();

		private int _mission;

		private readonly List<int> _rankDrop = new List<int>();

		private readonly List<int> _starLevel = new List<int>();

		private int _firstBloodDrop;

		private readonly List<JingYingFuBenPeiZhi.FirstbloodshowPair> _firstBloodShow = new List<JingYingFuBenPeiZhi.FirstbloodshowPair>();

		private int _fightTimes;

		private int _retryCost;

		private int _mapname;

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

		[ProtoMember(3, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "map", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int map
		{
			get
			{
				return this._map;
			}
			set
			{
				this._map = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "bossName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossName
		{
			get
			{
				return this._bossName;
			}
			set
			{
				this._bossName = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "bossintroduce", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossintroduce
		{
			get
			{
				return this._bossintroduce;
			}
			set
			{
				this._bossintroduce = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "bossPic", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossPic
		{
			get
			{
				return this._bossPic;
			}
			set
			{
				this._bossPic = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "rank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "step", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "copyId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int copyId
		{
			get
			{
				return this._copyId;
			}
			set
			{
				this._copyId = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "power", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int power
		{
			get
			{
				return this._power;
			}
			set
			{
				this._power = value;
			}
		}

		[ProtoMember(13, Name = "normalDrop", DataFormat = DataFormat.TwosComplement)]
		public List<int> normalDrop
		{
			get
			{
				return this._normalDrop;
			}
		}

		[ProtoMember(14, Name = "normalShow", DataFormat = DataFormat.Default)]
		public List<JingYingFuBenPeiZhi.NormalshowPair> normalShow
		{
			get
			{
				return this._normalShow;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "mission", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mission
		{
			get
			{
				return this._mission;
			}
			set
			{
				this._mission = value;
			}
		}

		[ProtoMember(16, Name = "rankDrop", DataFormat = DataFormat.TwosComplement)]
		public List<int> rankDrop
		{
			get
			{
				return this._rankDrop;
			}
		}

		[ProtoMember(17, Name = "starLevel", DataFormat = DataFormat.TwosComplement)]
		public List<int> starLevel
		{
			get
			{
				return this._starLevel;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "firstBloodDrop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int firstBloodDrop
		{
			get
			{
				return this._firstBloodDrop;
			}
			set
			{
				this._firstBloodDrop = value;
			}
		}

		[ProtoMember(20, Name = "firstBloodShow", DataFormat = DataFormat.Default)]
		public List<JingYingFuBenPeiZhi.FirstbloodshowPair> firstBloodShow
		{
			get
			{
				return this._firstBloodShow;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "fightTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fightTimes
		{
			get
			{
				return this._fightTimes;
			}
			set
			{
				this._fightTimes = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "retryCost", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int retryCost
		{
			get
			{
				return this._retryCost;
			}
			set
			{
				this._retryCost = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "mapname", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapname
		{
			get
			{
				return this._mapname;
			}
			set
			{
				this._mapname = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
