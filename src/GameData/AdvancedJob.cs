using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "AdvancedJob")]
	[Serializable]
	public class AdvancedJob : IExtensible
	{
		private int _id;

		private int _attrsDelta;

		private int _passiveSkill;

		private int _description;

		private int _skillExtendId;

		private int _advanced1Model;

		private readonly List<int> _stageId = new List<int>();

		private int _name;

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

		[ProtoMember(3, IsRequired = false, Name = "attrsDelta", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attrsDelta
		{
			get
			{
				return this._attrsDelta;
			}
			set
			{
				this._attrsDelta = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "passiveSkill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int passiveSkill
		{
			get
			{
				return this._passiveSkill;
			}
			set
			{
				this._passiveSkill = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "description", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "skillExtendId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillExtendId
		{
			get
			{
				return this._skillExtendId;
			}
			set
			{
				this._skillExtendId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "advanced1Model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int advanced1Model
		{
			get
			{
				return this._advanced1Model;
			}
			set
			{
				this._advanced1Model = value;
			}
		}

		[ProtoMember(8, Name = "stageId", DataFormat = DataFormat.TwosComplement)]
		public List<int> stageId
		{
			get
			{
				return this._stageId;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
