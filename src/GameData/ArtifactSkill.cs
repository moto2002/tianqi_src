using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ArtifactSkill")]
	[Serializable]
	public class ArtifactSkill : IExtensible
	{
		private int _id;

		private int _job;

		private int _lv;

		private int _skillType;

		private readonly List<int> _effect = new List<int>();

		private int _activation;

		private int _name;

		private int _desc;

		private int _icon;

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

		[ProtoMember(3, IsRequired = false, Name = "job", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int job
		{
			get
			{
				return this._job;
			}
			set
			{
				this._job = value;
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

		[ProtoMember(5, IsRequired = false, Name = "skillType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillType
		{
			get
			{
				return this._skillType;
			}
			set
			{
				this._skillType = value;
			}
		}

		[ProtoMember(6, Name = "effect", DataFormat = DataFormat.TwosComplement)]
		public List<int> effect
		{
			get
			{
				return this._effect;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "activation", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activation
		{
			get
			{
				return this._activation;
			}
			set
			{
				this._activation = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "desc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
