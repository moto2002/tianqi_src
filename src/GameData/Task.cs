using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Task")]
	[Serializable]
	public class Task : IExtensible
	{
		private int _id;

		private int _name;

		private int _target;

		private int _introduction;

		private int _linkInstance;

		private int _linkSystemTask;

		private int _linkNPC;

		private readonly List<int> _dialogue = new List<int>();

		private int _frontTask;

		private int _nextTask;

		private int _reward;

		private int _beginFMV;

		private int _endFMV;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "target", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "introduction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "linkInstance", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkInstance
		{
			get
			{
				return this._linkInstance;
			}
			set
			{
				this._linkInstance = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "linkSystemTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkSystemTask
		{
			get
			{
				return this._linkSystemTask;
			}
			set
			{
				this._linkSystemTask = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "linkNPC", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkNPC
		{
			get
			{
				return this._linkNPC;
			}
			set
			{
				this._linkNPC = value;
			}
		}

		[ProtoMember(8, Name = "dialogue", DataFormat = DataFormat.TwosComplement)]
		public List<int> dialogue
		{
			get
			{
				return this._dialogue;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "frontTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "nextTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "reward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reward
		{
			get
			{
				return this._reward;
			}
			set
			{
				this._reward = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "beginFMV", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int beginFMV
		{
			get
			{
				return this._beginFMV;
			}
			set
			{
				this._beginFMV = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "endFMV", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int endFMV
		{
			get
			{
				return this._endFMV;
			}
			set
			{
				this._endFMV = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
