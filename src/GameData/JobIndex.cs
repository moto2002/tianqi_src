using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"jobId"
	}), ProtoContract(Name = "JobIndex")]
	[Serializable]
	public class JobIndex : IExtensible
	{
		private int _jobId;

		private readonly List<int> _AdvancedJobId = new List<int>();

		private readonly List<int> _level = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "jobId", DataFormat = DataFormat.TwosComplement)]
		public int jobId
		{
			get
			{
				return this._jobId;
			}
			set
			{
				this._jobId = value;
			}
		}

		[ProtoMember(3, Name = "AdvancedJobId", DataFormat = DataFormat.TwosComplement)]
		public List<int> AdvancedJobId
		{
			get
			{
				return this._AdvancedJobId;
			}
		}

		[ProtoMember(4, Name = "level", DataFormat = DataFormat.TwosComplement)]
		public List<int> level
		{
			get
			{
				return this._level;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
