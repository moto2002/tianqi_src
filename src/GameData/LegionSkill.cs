using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "LegionSkill")]
	[Serializable]
	public class LegionSkill : IExtensible
	{
		private int _id;

		private int _level;

		private int _attrs;

		private int _name;

		private int _cost;

		private int _icon;

		private int _percentage;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "attrs", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attrs
		{
			get
			{
				return this._attrs;
			}
			set
			{
				this._attrs = value;
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

		[ProtoMember(6, IsRequired = false, Name = "cost", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cost
		{
			get
			{
				return this._cost;
			}
			set
			{
				this._cost = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "percentage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int percentage
		{
			get
			{
				return this._percentage;
			}
			set
			{
				this._percentage = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
