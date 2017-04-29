using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "LingChengRenWuKuPeiZhi")]
	[Serializable]
	public class LingChengRenWuKuPeiZhi : IExtensible
	{
		[ProtoContract(Name = "TasknumPair")]
		[Serializable]
		public class TasknumPair : IExtensible
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

		private int _taskBranch;

		private readonly List<LingChengRenWuKuPeiZhi.TasknumPair> _taskNum = new List<LingChengRenWuKuPeiZhi.TasknumPair>();

		private int _taskPic;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "taskBranch", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, Name = "taskNum", DataFormat = DataFormat.Default)]
		public List<LingChengRenWuKuPeiZhi.TasknumPair> taskNum
		{
			get
			{
				return this._taskNum;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "taskPic", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskPic
		{
			get
			{
				return this._taskPic;
			}
			set
			{
				this._taskPic = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
