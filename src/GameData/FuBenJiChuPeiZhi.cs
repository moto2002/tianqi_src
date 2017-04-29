using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "FuBenJiChuPeiZhi")]
	[Serializable]
	public class FuBenJiChuPeiZhi : IExtensible
	{
		private int _id;

		private int _type;

		private int _serverBorn;

		private int _defaultAiState;

		private int _startProcessAi;

		private int _sceneType;

		private int _scene;

		private int _autoFight;

		private int _petType;

		private int _time;

		private int _timingMode;

		private int _limit;

		private int _completeTarget;

		private readonly List<int> _targetValue = new List<int>();

		private int _failJudge;

		private int _uiDelay;

		private int _waitTime;

		private int _countdown;

		private int _endTime;

		private int _startMessage;

		private int _completeMessege;

		private int _extraBoss;

		private int _timeLine;

		private int _actionPoint;

		private int _pointId;

		private int _monsterBornDirection;

		private int _completeDelayTime;

		private readonly List<int> _refreshId = new List<int>();

		private int _switch1;

		private int _switch2;

		private int _waveShow;

		private int _countdownInfo;

		private int _revive;

		private readonly List<int> _revivePoint = new List<int>();

		private int _reviveTime;

		private readonly List<int> _reviveExpend = new List<int>();

		private readonly List<int> _beginSkill = new List<int>();

		private readonly List<int> _ghostMonsterTypeId = new List<int>();

		private readonly List<int> _ghostMonsterLevel = new List<int>();

		private readonly List<int> _ghostMonsterSpawnPoint = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "serverBorn", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int serverBorn
		{
			get
			{
				return this._serverBorn;
			}
			set
			{
				this._serverBorn = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "defaultAiState", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int defaultAiState
		{
			get
			{
				return this._defaultAiState;
			}
			set
			{
				this._defaultAiState = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "startProcessAi", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int startProcessAi
		{
			get
			{
				return this._startProcessAi;
			}
			set
			{
				this._startProcessAi = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "sceneType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sceneType
		{
			get
			{
				return this._sceneType;
			}
			set
			{
				this._sceneType = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "scene", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int scene
		{
			get
			{
				return this._scene;
			}
			set
			{
				this._scene = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "autoFight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int autoFight
		{
			get
			{
				return this._autoFight;
			}
			set
			{
				this._autoFight = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "petType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petType
		{
			get
			{
				return this._petType;
			}
			set
			{
				this._petType = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "timingMode", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int timingMode
		{
			get
			{
				return this._timingMode;
			}
			set
			{
				this._timingMode = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "limit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int limit
		{
			get
			{
				return this._limit;
			}
			set
			{
				this._limit = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "completeTarget", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int completeTarget
		{
			get
			{
				return this._completeTarget;
			}
			set
			{
				this._completeTarget = value;
			}
		}

		[ProtoMember(16, Name = "targetValue", DataFormat = DataFormat.TwosComplement)]
		public List<int> targetValue
		{
			get
			{
				return this._targetValue;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "failJudge", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int failJudge
		{
			get
			{
				return this._failJudge;
			}
			set
			{
				this._failJudge = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "uiDelay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int uiDelay
		{
			get
			{
				return this._uiDelay;
			}
			set
			{
				this._uiDelay = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "waitTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int waitTime
		{
			get
			{
				return this._waitTime;
			}
			set
			{
				this._waitTime = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "countdown", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int countdown
		{
			get
			{
				return this._countdown;
			}
			set
			{
				this._countdown = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "startMessage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int startMessage
		{
			get
			{
				return this._startMessage;
			}
			set
			{
				this._startMessage = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "completeMessege", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int completeMessege
		{
			get
			{
				return this._completeMessege;
			}
			set
			{
				this._completeMessege = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "extraBoss", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int extraBoss
		{
			get
			{
				return this._extraBoss;
			}
			set
			{
				this._extraBoss = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "timeLine", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int timeLine
		{
			get
			{
				return this._timeLine;
			}
			set
			{
				this._timeLine = value;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "actionPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int actionPoint
		{
			get
			{
				return this._actionPoint;
			}
			set
			{
				this._actionPoint = value;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "pointId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pointId
		{
			get
			{
				return this._pointId;
			}
			set
			{
				this._pointId = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "monsterBornDirection", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterBornDirection
		{
			get
			{
				return this._monsterBornDirection;
			}
			set
			{
				this._monsterBornDirection = value;
			}
		}

		[ProtoMember(29, IsRequired = false, Name = "completeDelayTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int completeDelayTime
		{
			get
			{
				return this._completeDelayTime;
			}
			set
			{
				this._completeDelayTime = value;
			}
		}

		[ProtoMember(30, Name = "refreshId", DataFormat = DataFormat.TwosComplement)]
		public List<int> refreshId
		{
			get
			{
				return this._refreshId;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "switch1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int switch1
		{
			get
			{
				return this._switch1;
			}
			set
			{
				this._switch1 = value;
			}
		}

		[ProtoMember(32, IsRequired = false, Name = "switch2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int switch2
		{
			get
			{
				return this._switch2;
			}
			set
			{
				this._switch2 = value;
			}
		}

		[ProtoMember(33, IsRequired = false, Name = "waveShow", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int waveShow
		{
			get
			{
				return this._waveShow;
			}
			set
			{
				this._waveShow = value;
			}
		}

		[ProtoMember(34, IsRequired = false, Name = "countdownInfo", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int countdownInfo
		{
			get
			{
				return this._countdownInfo;
			}
			set
			{
				this._countdownInfo = value;
			}
		}

		[ProtoMember(35, IsRequired = false, Name = "revive", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int revive
		{
			get
			{
				return this._revive;
			}
			set
			{
				this._revive = value;
			}
		}

		[ProtoMember(36, Name = "revivePoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> revivePoint
		{
			get
			{
				return this._revivePoint;
			}
		}

		[ProtoMember(37, IsRequired = false, Name = "reviveTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reviveTime
		{
			get
			{
				return this._reviveTime;
			}
			set
			{
				this._reviveTime = value;
			}
		}

		[ProtoMember(38, Name = "reviveExpend", DataFormat = DataFormat.TwosComplement)]
		public List<int> reviveExpend
		{
			get
			{
				return this._reviveExpend;
			}
		}

		[ProtoMember(39, Name = "beginSkill", DataFormat = DataFormat.TwosComplement)]
		public List<int> beginSkill
		{
			get
			{
				return this._beginSkill;
			}
		}

		[ProtoMember(40, Name = "ghostMonsterTypeId", DataFormat = DataFormat.TwosComplement)]
		public List<int> ghostMonsterTypeId
		{
			get
			{
				return this._ghostMonsterTypeId;
			}
		}

		[ProtoMember(41, Name = "ghostMonsterLevel", DataFormat = DataFormat.TwosComplement)]
		public List<int> ghostMonsterLevel
		{
			get
			{
				return this._ghostMonsterLevel;
			}
		}

		[ProtoMember(42, Name = "ghostMonsterSpawnPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> ghostMonsterSpawnPoint
		{
			get
			{
				return this._ghostMonsterSpawnPoint;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
