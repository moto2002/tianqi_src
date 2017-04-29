using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GuaJiQuYuPeiZhi")]
	[Serializable]
	public class GuaJiQuYuPeiZhi : IExtensible
	{
		private int _id;

		private int _areaType;

		private int _setMap;

		private readonly List<int> _coordinates = new List<int>();

		private int _rooms;

		private int _condition;

		private int _copyId;

		private long _exp;

		private int _expScale;

		private int _refreshId;

		private int _monsterLv;

		private int _refreshBossId;

		private int _probability;

		private int _battleMode;

		private int _switch;

		private int _pk;

		private int _revenge;

		private readonly List<int> _dropId = new List<int>();

		private readonly List<int> _dropBossId = new List<int>();

		private readonly List<int> _killdrop = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "areaType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int areaType
		{
			get
			{
				return this._areaType;
			}
			set
			{
				this._areaType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "setMap", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int setMap
		{
			get
			{
				return this._setMap;
			}
			set
			{
				this._setMap = value;
			}
		}

		[ProtoMember(5, Name = "coordinates", DataFormat = DataFormat.TwosComplement)]
		public List<int> coordinates
		{
			get
			{
				return this._coordinates;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "rooms", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rooms
		{
			get
			{
				return this._rooms;
			}
			set
			{
				this._rooms = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "condition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int condition
		{
			get
			{
				return this._condition;
			}
			set
			{
				this._condition = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "copyId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int copyId
		{
			get
			{
				return this._copyId;
			}
			set
			{
				this._copyId = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "exp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long exp
		{
			get
			{
				return this._exp;
			}
			set
			{
				this._exp = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "expScale", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int expScale
		{
			get
			{
				return this._expScale;
			}
			set
			{
				this._expScale = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "refreshId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshId
		{
			get
			{
				return this._refreshId;
			}
			set
			{
				this._refreshId = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "monsterLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "refreshBossId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshBossId
		{
			get
			{
				return this._refreshBossId;
			}
			set
			{
				this._refreshBossId = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "battleMode", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int battleMode
		{
			get
			{
				return this._battleMode;
			}
			set
			{
				this._battleMode = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "switch", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int @switch
		{
			get
			{
				return this._switch;
			}
			set
			{
				this._switch = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "pk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pk
		{
			get
			{
				return this._pk;
			}
			set
			{
				this._pk = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "revenge", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int revenge
		{
			get
			{
				return this._revenge;
			}
			set
			{
				this._revenge = value;
			}
		}

		[ProtoMember(19, Name = "dropId", DataFormat = DataFormat.TwosComplement)]
		public List<int> dropId
		{
			get
			{
				return this._dropId;
			}
		}

		[ProtoMember(20, Name = "dropBossId", DataFormat = DataFormat.TwosComplement)]
		public List<int> dropBossId
		{
			get
			{
				return this._dropBossId;
			}
		}

		[ProtoMember(21, Name = "killdrop", DataFormat = DataFormat.TwosComplement)]
		public List<int> killdrop
		{
			get
			{
				return this._killdrop;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
