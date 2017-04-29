using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"behaviorId"
	}), ProtoContract(Name = "Behavior")]
	[Serializable]
	public class Behavior : IExtensible
	{
		private int _behaviorId;

		private int _target;

		private int _summon;

		private int _fuse;

		private int _useskill;

		private readonly List<int> _talk = new List<int>();

		private int _addAI;

		private int _deleteAI;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "behaviorId", DataFormat = DataFormat.TwosComplement)]
		public int behaviorId
		{
			get
			{
				return this._behaviorId;
			}
			set
			{
				this._behaviorId = value;
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

		[ProtoMember(4, IsRequired = false, Name = "summon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int summon
		{
			get
			{
				return this._summon;
			}
			set
			{
				this._summon = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "fuse", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fuse
		{
			get
			{
				return this._fuse;
			}
			set
			{
				this._fuse = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "useskill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int useskill
		{
			get
			{
				return this._useskill;
			}
			set
			{
				this._useskill = value;
			}
		}

		[ProtoMember(7, Name = "talk", DataFormat = DataFormat.TwosComplement)]
		public List<int> talk
		{
			get
			{
				return this._talk;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "addAI", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int addAI
		{
			get
			{
				return this._addAI;
			}
			set
			{
				this._addAI = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "deleteAI", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deleteAI
		{
			get
			{
				return this._deleteAI;
			}
			set
			{
				this._deleteAI = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
