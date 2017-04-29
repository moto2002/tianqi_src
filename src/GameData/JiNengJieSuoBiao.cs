using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JiNengJieSuoBiao")]
	[Serializable]
	public class JiNengJieSuoBiao : IExtensible
	{
		private int _groupId;

		private int _typeId;

		private int _sortNum;

		private readonly List<int> _deblockingType = new List<int>();

		private readonly List<int> _condition = new List<int>();

		private int _deblockingDesc;

		private int _skillId;

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

		[ProtoMember(3, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "sortNum", DataFormat = DataFormat.TwosComplement)]
		public int sortNum
		{
			get
			{
				return this._sortNum;
			}
			set
			{
				this._sortNum = value;
			}
		}

		[ProtoMember(5, Name = "deblockingType", DataFormat = DataFormat.TwosComplement)]
		public List<int> deblockingType
		{
			get
			{
				return this._deblockingType;
			}
		}

		[ProtoMember(6, Name = "condition", DataFormat = DataFormat.TwosComplement)]
		public List<int> condition
		{
			get
			{
				return this._condition;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "deblockingDesc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deblockingDesc
		{
			get
			{
				return this._deblockingDesc;
			}
			set
			{
				this._deblockingDesc = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "skillId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
