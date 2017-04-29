using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(780), ForSend(780), ProtoContract(Name = "BountyLoginPush")]
	[Serializable]
	public class BountyLoginPush : IExtensible
	{
		public static readonly short OP = 780;

		private readonly List<ProductionInfo> _productions = new List<ProductionInfo>();

		private int _taskId;

		private int _freeCountDown;

		private int _score;

		private int _hasStar;

		private int _urgentTaskId;

		private int _hasStarUrgent;

		private int _urgentTaskOpenTime;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "productions", DataFormat = DataFormat.Default)]
		public List<ProductionInfo> productions
		{
			get
			{
				return this._productions;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "taskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskId
		{
			get
			{
				return this._taskId;
			}
			set
			{
				this._taskId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "freeCountDown", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int freeCountDown
		{
			get
			{
				return this._freeCountDown;
			}
			set
			{
				this._freeCountDown = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "score", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int score
		{
			get
			{
				return this._score;
			}
			set
			{
				this._score = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "hasStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hasStar
		{
			get
			{
				return this._hasStar;
			}
			set
			{
				this._hasStar = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "urgentTaskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int urgentTaskId
		{
			get
			{
				return this._urgentTaskId;
			}
			set
			{
				this._urgentTaskId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "hasStarUrgent", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hasStarUrgent
		{
			get
			{
				return this._hasStarUrgent;
			}
			set
			{
				this._hasStarUrgent = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "urgentTaskOpenTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int urgentTaskOpenTime
		{
			get
			{
				return this._urgentTaskOpenTime;
			}
			set
			{
				this._urgentTaskOpenTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
