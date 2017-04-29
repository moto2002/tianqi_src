using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "PetTaskInfo")]
	[Serializable]
	public class PetTaskInfo : IExtensible
	{
		[ProtoContract(Name = "PetTaskStatus")]
		public enum PetTaskStatus
		{
			[ProtoEnum(Name = "UnPickUp", Value = 1)]
			UnPickUp = 1,
			[ProtoEnum(Name = "undone", Value = 2)]
			undone,
			[ProtoEnum(Name = "achieve", Value = 3)]
			achieve,
			[ProtoEnum(Name = "receive", Value = 4)]
			receive
		}

		private long _idx;

		private int _taskId;

		private readonly List<int> _monsterId = new List<int>();

		private readonly List<int> _petId = new List<int>();

		private int _taskName;

		private PetTaskInfo.PetTaskStatus _Status = PetTaskInfo.PetTaskStatus.UnPickUp;

		private int _times;

		private readonly List<int> _choosePets = new List<int>();

		private readonly List<ItemBriefInfo> _rewards = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "idx", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
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

		[ProtoMember(3, Name = "monsterId", DataFormat = DataFormat.TwosComplement)]
		public List<int> monsterId
		{
			get
			{
				return this._monsterId;
			}
		}

		[ProtoMember(4, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public List<int> petId
		{
			get
			{
				return this._petId;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "taskName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskName
		{
			get
			{
				return this._taskName;
			}
			set
			{
				this._taskName = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "Status", DataFormat = DataFormat.TwosComplement), DefaultValue(PetTaskInfo.PetTaskStatus.UnPickUp)]
		public PetTaskInfo.PetTaskStatus Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "times", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		[ProtoMember(8, Name = "choosePets", DataFormat = DataFormat.TwosComplement)]
		public List<int> choosePets
		{
			get
			{
				return this._choosePets;
			}
		}

		[ProtoMember(9, Name = "rewards", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> rewards
		{
			get
			{
				return this._rewards;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
