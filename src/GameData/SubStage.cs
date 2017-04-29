using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"subId"
	}), ProtoContract(Name = "SubStage")]
	[Serializable]
	public class SubStage : IExtensible
	{
		private int _subId;

		private readonly List<int> _taskid1 = new List<int>();

		private readonly List<int> _taskid2 = new List<int>();

		private readonly List<int> _taskid3 = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "subId", DataFormat = DataFormat.TwosComplement)]
		public int subId
		{
			get
			{
				return this._subId;
			}
			set
			{
				this._subId = value;
			}
		}

		[ProtoMember(3, Name = "taskid1", DataFormat = DataFormat.TwosComplement)]
		public List<int> taskid1
		{
			get
			{
				return this._taskid1;
			}
		}

		[ProtoMember(4, Name = "taskid2", DataFormat = DataFormat.TwosComplement)]
		public List<int> taskid2
		{
			get
			{
				return this._taskid2;
			}
		}

		[ProtoMember(5, Name = "taskid3", DataFormat = DataFormat.TwosComplement)]
		public List<int> taskid3
		{
			get
			{
				return this._taskid3;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
