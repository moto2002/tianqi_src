using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"monsterRefreshId",
		"id"
	}), ProtoContract(Name = "MonsterRefresh")]
	[Serializable]
	public class MonsterRefresh : IExtensible
	{
		[ProtoContract(Name = "DialoguenpcPair")]
		[Serializable]
		public class DialoguenpcPair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

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

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
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

		[ProtoContract(Name = "DialoguelasttimePair")]
		[Serializable]
		public class DialoguelasttimePair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

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

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
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

		private int _monsterRefreshId;

		private int _id;

		private int _batch;

		private int _refreshType;

		private int _monster;

		private int _refreshLv;

		private int _monsterLv;

		private int _minMass;

		private int _alwaysExist;

		private int _dieBatch;

		private int _selfType;

		private int _protectNpc;

		private int _bornDelay;

		private int _bornPoint;

		private int _time;

		private int _num;

		private int _target;

		private int _cameraType;

		private int _way;

		private int _triggerArea;

		private string _aiId = string.Empty;

		private readonly List<int> _eventOne = new List<int>();

		private readonly List<int> _eventTwo = new List<int>();

		private string _event = string.Empty;

		private string _camera = string.Empty;

		private readonly List<int> _dialogue = new List<int>();

		private readonly List<int> _dialogueTime = new List<int>();

		private int _dialogueMonster;

		private int _npcBorn;

		private readonly List<int> _dialogueMoment = new List<int>();

		private readonly List<int> _refreshBatch = new List<int>();

		private readonly List<MonsterRefresh.DialoguenpcPair> _dialogueNpc = new List<MonsterRefresh.DialoguenpcPair>();

		private readonly List<MonsterRefresh.DialoguelasttimePair> _dialogueLastTime = new List<MonsterRefresh.DialoguelasttimePair>();

		private int _dialogueId;

		private IExtension extensionObject;

		[ProtoMember(4, IsRequired = true, Name = "monsterRefreshId", DataFormat = DataFormat.TwosComplement)]
		public int monsterRefreshId
		{
			get
			{
				return this._monsterRefreshId;
			}
			set
			{
				this._monsterRefreshId = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(6, IsRequired = false, Name = "batch", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int batch
		{
			get
			{
				return this._batch;
			}
			set
			{
				this._batch = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "refreshType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshType
		{
			get
			{
				return this._refreshType;
			}
			set
			{
				this._refreshType = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "monster", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monster
		{
			get
			{
				return this._monster;
			}
			set
			{
				this._monster = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "refreshLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshLv
		{
			get
			{
				return this._refreshLv;
			}
			set
			{
				this._refreshLv = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "monsterLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterLv
		{
			get
			{
				return this._monsterLv;
			}
			set
			{
				this._monsterLv = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "minMass", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minMass
		{
			get
			{
				return this._minMass;
			}
			set
			{
				this._minMass = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "alwaysExist", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int alwaysExist
		{
			get
			{
				return this._alwaysExist;
			}
			set
			{
				this._alwaysExist = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "dieBatch", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dieBatch
		{
			get
			{
				return this._dieBatch;
			}
			set
			{
				this._dieBatch = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "selfType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int selfType
		{
			get
			{
				return this._selfType;
			}
			set
			{
				this._selfType = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "protectNpc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int protectNpc
		{
			get
			{
				return this._protectNpc;
			}
			set
			{
				this._protectNpc = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "bornDelay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bornDelay
		{
			get
			{
				return this._bornDelay;
			}
			set
			{
				this._bornDelay = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "bornPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bornPoint
		{
			get
			{
				return this._bornPoint;
			}
			set
			{
				this._bornPoint = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "target", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(22, IsRequired = false, Name = "cameraType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cameraType
		{
			get
			{
				return this._cameraType;
			}
			set
			{
				this._cameraType = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "way", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int way
		{
			get
			{
				return this._way;
			}
			set
			{
				this._way = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "triggerArea", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int triggerArea
		{
			get
			{
				return this._triggerArea;
			}
			set
			{
				this._triggerArea = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "aiId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string aiId
		{
			get
			{
				return this._aiId;
			}
			set
			{
				this._aiId = value;
			}
		}

		[ProtoMember(26, Name = "eventOne", DataFormat = DataFormat.TwosComplement)]
		public List<int> eventOne
		{
			get
			{
				return this._eventOne;
			}
		}

		[ProtoMember(27, Name = "eventTwo", DataFormat = DataFormat.TwosComplement)]
		public List<int> eventTwo
		{
			get
			{
				return this._eventTwo;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "event", DataFormat = DataFormat.Default), DefaultValue("")]
		public string @event
		{
			get
			{
				return this._event;
			}
			set
			{
				this._event = value;
			}
		}

		[ProtoMember(29, IsRequired = false, Name = "camera", DataFormat = DataFormat.Default), DefaultValue("")]
		public string camera
		{
			get
			{
				return this._camera;
			}
			set
			{
				this._camera = value;
			}
		}

		[ProtoMember(30, Name = "dialogue", DataFormat = DataFormat.TwosComplement)]
		public List<int> dialogue
		{
			get
			{
				return this._dialogue;
			}
		}

		[ProtoMember(31, Name = "dialogueTime", DataFormat = DataFormat.TwosComplement)]
		public List<int> dialogueTime
		{
			get
			{
				return this._dialogueTime;
			}
		}

		[ProtoMember(32, IsRequired = false, Name = "dialogueMonster", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dialogueMonster
		{
			get
			{
				return this._dialogueMonster;
			}
			set
			{
				this._dialogueMonster = value;
			}
		}

		[ProtoMember(33, IsRequired = false, Name = "npcBorn", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int npcBorn
		{
			get
			{
				return this._npcBorn;
			}
			set
			{
				this._npcBorn = value;
			}
		}

		[ProtoMember(34, Name = "dialogueMoment", DataFormat = DataFormat.TwosComplement)]
		public List<int> dialogueMoment
		{
			get
			{
				return this._dialogueMoment;
			}
		}

		[ProtoMember(35, Name = "refreshBatch", DataFormat = DataFormat.TwosComplement)]
		public List<int> refreshBatch
		{
			get
			{
				return this._refreshBatch;
			}
		}

		[ProtoMember(36, Name = "dialogueNpc", DataFormat = DataFormat.Default)]
		public List<MonsterRefresh.DialoguenpcPair> dialogueNpc
		{
			get
			{
				return this._dialogueNpc;
			}
		}

		[ProtoMember(37, Name = "dialogueLastTime", DataFormat = DataFormat.Default)]
		public List<MonsterRefresh.DialoguelasttimePair> dialogueLastTime
		{
			get
			{
				return this._dialogueLastTime;
			}
		}

		[ProtoMember(38, IsRequired = false, Name = "dialogueId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dialogueId
		{
			get
			{
				return this._dialogueId;
			}
			set
			{
				this._dialogueId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
