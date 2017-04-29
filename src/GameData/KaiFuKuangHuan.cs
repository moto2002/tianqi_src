using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "KaiFuKuangHuan")]
	[Serializable]
	public class KaiFuKuangHuan : IExtensible
	{
		private int _taskId;

		private int _taskSubType;

		private int _taskType;

		private int _finishPar1;

		private int _finishPar2;

		private int _openDay;

		private readonly List<int> _rewardItemId = new List<int>();

		private readonly List<int> _role = new List<int>();

		private readonly List<long> _itemNum = new List<long>();

		private int _goButton;

		private int _described;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "taskId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "taskSubType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskSubType
		{
			get
			{
				return this._taskSubType;
			}
			set
			{
				this._taskSubType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "taskType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "finishPar1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int finishPar1
		{
			get
			{
				return this._finishPar1;
			}
			set
			{
				this._finishPar1 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "finishPar2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int finishPar2
		{
			get
			{
				return this._finishPar2;
			}
			set
			{
				this._finishPar2 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "openDay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openDay
		{
			get
			{
				return this._openDay;
			}
			set
			{
				this._openDay = value;
			}
		}

		[ProtoMember(8, Name = "rewardItemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardItemId
		{
			get
			{
				return this._rewardItemId;
			}
		}

		[ProtoMember(9, Name = "role", DataFormat = DataFormat.TwosComplement)]
		public List<int> role
		{
			get
			{
				return this._role;
			}
		}

		[ProtoMember(10, Name = "itemNum", DataFormat = DataFormat.TwosComplement)]
		public List<long> itemNum
		{
			get
			{
				return this._itemNum;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "goButton", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goButton
		{
			get
			{
				return this._goButton;
			}
			set
			{
				this._goButton = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "described", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int described
		{
			get
			{
				return this._described;
			}
			set
			{
				this._described = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
