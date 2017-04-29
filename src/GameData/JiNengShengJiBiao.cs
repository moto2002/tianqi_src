using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JiNengShengJiBiao")]
	[Serializable]
	public class JiNengShengJiBiao : IExtensible
	{
		private int _groupId;

		private int _skillId;

		private int _lv;

		private int _lvLmt;

		private int _equivalentLv;

		private int _upgradeMode;

		private readonly List<int> _itemId = new List<int>();

		private readonly List<int> _itemNum = new List<int>();

		private int _desc1;

		private float _skillDate1;

		private int _desc2;

		private int _skillDate2;

		private int _desc3;

		private int _skillDate3;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "groupId", DataFormat = DataFormat.TwosComplement)]
		public int groupId
		{
			get
			{
				return this._groupId;
			}
			set
			{
				this._groupId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = false, Name = "lvLmt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lvLmt
		{
			get
			{
				return this._lvLmt;
			}
			set
			{
				this._lvLmt = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "equivalentLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equivalentLv
		{
			get
			{
				return this._equivalentLv;
			}
			set
			{
				this._equivalentLv = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "upgradeMode", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int upgradeMode
		{
			get
			{
				return this._upgradeMode;
			}
			set
			{
				this._upgradeMode = value;
			}
		}

		[ProtoMember(8, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemId
		{
			get
			{
				return this._itemId;
			}
		}

		[ProtoMember(9, Name = "itemNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemNum
		{
			get
			{
				return this._itemNum;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "desc1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc1
		{
			get
			{
				return this._desc1;
			}
			set
			{
				this._desc1 = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "skillDate1", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float skillDate1
		{
			get
			{
				return this._skillDate1;
			}
			set
			{
				this._skillDate1 = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "desc2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc2
		{
			get
			{
				return this._desc2;
			}
			set
			{
				this._desc2 = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "skillDate2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillDate2
		{
			get
			{
				return this._skillDate2;
			}
			set
			{
				this._skillDate2 = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "desc3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc3
		{
			get
			{
				return this._desc3;
			}
			set
			{
				this._desc3 = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "skillDate3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillDate3
		{
			get
			{
				return this._skillDate3;
			}
			set
			{
				this._skillDate3 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
