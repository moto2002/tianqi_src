using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "RenWuPeiZhi")]
	[Serializable]
	public class RenWuPeiZhi : IExtensible
	{
		[ProtoContract(Name = "RewardPair")]
		[Serializable]
		public class RewardPair : IExtensible
		{
			private int _key;

			private long _value;

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

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
			public long value
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

		private int _taskType;

		private int _frontTask;

		private int _nextTask;

		private int _lv;

		private int _maxLv;

		private int _dramaIntroduce;

		private int _targetContent;

		private int _introduction;

		private readonly List<int> _dialogue1 = new List<int>();

		private readonly List<int> _dialogue2 = new List<int>();

		private int _linkNpc1;

		private int _linkNpc2;

		private int _instanceNpc;

		private int _type;

		private readonly List<int> _target = new List<int>();

		private readonly List<int> _rewardId = new List<int>();

		private readonly List<RenWuPeiZhi.RewardPair> _reward = new List<RenWuPeiZhi.RewardPair>();

		private int _doubleReward;

		private readonly List<int> _lens = new List<int>();

		private readonly List<int> _pathfinding = new List<int>();

		private readonly List<int> _transferPoint = new List<int>();

		private int _quickReceive;

		private int _quickComplete;

		private int _quickAchieve;

		private int _taskBranch;

		private int _expediteForward;

		private int _stickie;

		private int _finishEffect;

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

		[ProtoMember(3, IsRequired = false, Name = "taskType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskType
		{
			get
			{
				return this._taskType;
			}
			set
			{
				this._taskType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "frontTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frontTask
		{
			get
			{
				return this._frontTask;
			}
			set
			{
				this._frontTask = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "nextTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nextTask
		{
			get
			{
				return this._nextTask;
			}
			set
			{
				this._nextTask = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "maxLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxLv
		{
			get
			{
				return this._maxLv;
			}
			set
			{
				this._maxLv = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "dramaIntroduce", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dramaIntroduce
		{
			get
			{
				return this._dramaIntroduce;
			}
			set
			{
				this._dramaIntroduce = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "targetContent", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetContent
		{
			get
			{
				return this._targetContent;
			}
			set
			{
				this._targetContent = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "introduction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(12, Name = "dialogue1", DataFormat = DataFormat.TwosComplement)]
		public List<int> dialogue1
		{
			get
			{
				return this._dialogue1;
			}
		}

		[ProtoMember(13, Name = "dialogue2", DataFormat = DataFormat.TwosComplement)]
		public List<int> dialogue2
		{
			get
			{
				return this._dialogue2;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "linkNpc1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkNpc1
		{
			get
			{
				return this._linkNpc1;
			}
			set
			{
				this._linkNpc1 = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "linkNpc2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkNpc2
		{
			get
			{
				return this._linkNpc2;
			}
			set
			{
				this._linkNpc2 = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "instanceNpc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int instanceNpc
		{
			get
			{
				return this._instanceNpc;
			}
			set
			{
				this._instanceNpc = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(19, Name = "target", DataFormat = DataFormat.TwosComplement)]
		public List<int> target
		{
			get
			{
				return this._target;
			}
		}

		[ProtoMember(20, Name = "rewardId", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardId
		{
			get
			{
				return this._rewardId;
			}
		}

		[ProtoMember(21, Name = "reward", DataFormat = DataFormat.Default)]
		public List<RenWuPeiZhi.RewardPair> reward
		{
			get
			{
				return this._reward;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "doubleReward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int doubleReward
		{
			get
			{
				return this._doubleReward;
			}
			set
			{
				this._doubleReward = value;
			}
		}

		[ProtoMember(23, Name = "lens", DataFormat = DataFormat.TwosComplement)]
		public List<int> lens
		{
			get
			{
				return this._lens;
			}
		}

		[ProtoMember(24, Name = "pathfinding", DataFormat = DataFormat.TwosComplement)]
		public List<int> pathfinding
		{
			get
			{
				return this._pathfinding;
			}
		}

		[ProtoMember(25, Name = "transferPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> transferPoint
		{
			get
			{
				return this._transferPoint;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "quickReceive", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quickReceive
		{
			get
			{
				return this._quickReceive;
			}
			set
			{
				this._quickReceive = value;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "quickComplete", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quickComplete
		{
			get
			{
				return this._quickComplete;
			}
			set
			{
				this._quickComplete = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "quickAchieve", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quickAchieve
		{
			get
			{
				return this._quickAchieve;
			}
			set
			{
				this._quickAchieve = value;
			}
		}

		[ProtoMember(29, IsRequired = false, Name = "taskBranch", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskBranch
		{
			get
			{
				return this._taskBranch;
			}
			set
			{
				this._taskBranch = value;
			}
		}

		[ProtoMember(30, IsRequired = false, Name = "expediteForward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int expediteForward
		{
			get
			{
				return this._expediteForward;
			}
			set
			{
				this._expediteForward = value;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "stickie", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int stickie
		{
			get
			{
				return this._stickie;
			}
			set
			{
				this._stickie = value;
			}
		}

		[ProtoMember(32, IsRequired = false, Name = "finishEffect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int finishEffect
		{
			get
			{
				return this._finishEffect;
			}
			set
			{
				this._finishEffect = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
