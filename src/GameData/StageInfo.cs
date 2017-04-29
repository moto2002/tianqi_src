using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"stageId"
	}), ProtoContract(Name = "StageInfo")]
	[Serializable]
	public class StageInfo : IExtensible
	{
		private int _stageId;

		private readonly List<int> _taskid = new List<int>();

		private readonly List<int> _description = new List<int>();

		private int _title;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "stageId", DataFormat = DataFormat.TwosComplement)]
		public int stageId
		{
			get
			{
				return this._stageId;
			}
			set
			{
				this._stageId = value;
			}
		}

		[ProtoMember(3, Name = "taskid", DataFormat = DataFormat.TwosComplement)]
		public List<int> taskid
		{
			get
			{
				return this._taskid;
			}
		}

		[ProtoMember(4, Name = "description", DataFormat = DataFormat.TwosComplement)]
		public List<int> description
		{
			get
			{
				return this._description;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "title", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
