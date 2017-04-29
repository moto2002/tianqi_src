using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Guide")]
	[Serializable]
	public class Guide : IExtensible
	{
		private int _id;

		private int _off;

		private int _priority;

		private readonly List<int> _preguide = new List<int>();

		private int _triggerType;

		private int _Operator;

		private readonly List<int> _args = new List<int>();

		private readonly List<int> _levelRange = new List<int>();

		private int _finishTaskId;

		private int _finishInstanceId;

		private int _finishGuideId;

		private int _guideTimes;

		private int _lockWaitNext;

		private int _stopAutoRun;

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

		[ProtoMember(4, IsRequired = false, Name = "off", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int off
		{
			get
			{
				return this._off;
			}
			set
			{
				this._off = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "priority", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				this._priority = value;
			}
		}

		[ProtoMember(6, Name = "preguide", DataFormat = DataFormat.TwosComplement)]
		public List<int> preguide
		{
			get
			{
				return this._preguide;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "triggerType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int triggerType
		{
			get
			{
				return this._triggerType;
			}
			set
			{
				this._triggerType = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "Operator", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Operator
		{
			get
			{
				return this._Operator;
			}
			set
			{
				this._Operator = value;
			}
		}

		[ProtoMember(9, Name = "args", DataFormat = DataFormat.TwosComplement)]
		public List<int> args
		{
			get
			{
				return this._args;
			}
		}

		[ProtoMember(10, Name = "levelRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> levelRange
		{
			get
			{
				return this._levelRange;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "finishTaskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int finishTaskId
		{
			get
			{
				return this._finishTaskId;
			}
			set
			{
				this._finishTaskId = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "finishInstanceId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int finishInstanceId
		{
			get
			{
				return this._finishInstanceId;
			}
			set
			{
				this._finishInstanceId = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "finishGuideId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int finishGuideId
		{
			get
			{
				return this._finishGuideId;
			}
			set
			{
				this._finishGuideId = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "guideTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int guideTimes
		{
			get
			{
				return this._guideTimes;
			}
			set
			{
				this._guideTimes = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "lockWaitNext", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lockWaitNext
		{
			get
			{
				return this._lockWaitNext;
			}
			set
			{
				this._lockWaitNext = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "stopAutoRun", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int stopAutoRun
		{
			get
			{
				return this._stopAutoRun;
			}
			set
			{
				this._stopAutoRun = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
