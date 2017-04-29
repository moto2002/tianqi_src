using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "RenWu")]
	[Serializable]
	public class RenWu : IExtensible
	{
		private int _id;

		private int _chapter;

		private int _lv;

		private int _name;

		private int _target;

		private int _introduction;

		private int _linkInstance;

		private int _linkSystemTask;

		private int _linkNPC;

		private int _completeTime;

		private readonly List<int> _dialogue = new List<int>();

		private int _dramaIntroduce;

		private int _frontTask;

		private int _nextTask;

		private int _reward;

		private readonly List<int> _rewardItem = new List<int>();

		private readonly List<int> _rewardNum = new List<int>();

		private readonly List<int> _beginAnime = new List<int>();

		private readonly List<int> _endAnime = new List<int>();

		private int _dailyTask;

		private int _npcDialogue;

		private int _sprogId;

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

		[ProtoMember(3, IsRequired = false, Name = "chapter", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapter
		{
			get
			{
				return this._chapter;
			}
			set
			{
				this._chapter = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "target", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "introduction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "linkInstance", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "linkSystemTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "linkNPC", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "completeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int completeTime
		{
			get
			{
				return this._completeTime;
			}
			set
			{
				this._completeTime = value;
			}
		}

		[ProtoMember(12, Name = "dialogue", DataFormat = DataFormat.TwosComplement)]
		public List<int> dialogue
		{
			get
			{
				return this._dialogue;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "dramaIntroduce", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dramaIntroduce
		{
			get
			{
				return this._dramaIntroduce;
			}
			set
			{
				this._dramaIntroduce = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "frontTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(16, IsRequired = false, Name = "nextTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(17, IsRequired = false, Name = "reward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(18, Name = "rewardItem", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardItem
		{
			get
			{
				return this._rewardItem;
			}
		}

		[ProtoMember(19, Name = "rewardNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardNum
		{
			get
			{
				return this._rewardNum;
			}
		}

		[ProtoMember(20, Name = "beginAnime", DataFormat = DataFormat.TwosComplement)]
		public List<int> beginAnime
		{
			get
			{
				return this._beginAnime;
			}
		}

		[ProtoMember(21, Name = "endAnime", DataFormat = DataFormat.TwosComplement)]
		public List<int> endAnime
		{
			get
			{
				return this._endAnime;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "dailyTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dailyTask
		{
			get
			{
				return this._dailyTask;
			}
			set
			{
				this._dailyTask = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "npcDialogue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int npcDialogue
		{
			get
			{
				return this._npcDialogue;
			}
			set
			{
				this._npcDialogue = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "sprogId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sprogId
		{
			get
			{
				return this._sprogId;
			}
			set
			{
				this._sprogId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
