using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "SystemOpen")]
	[Serializable]
	public class SystemOpen : IExtensible
	{
		private int _id;

		private int _level;

		private int _levelClose;

		private int _taskId;

		private int _artifactId;

		private int _effect;

		private int _name;

		private int _icon;

		private int _icon2;

		private int _bewrite;

		private int _area;

		private int _areaIndex;

		private int _widgetId;

		private int _notice;

		private int _description;

		private int _description2;

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

		[ProtoMember(4, IsRequired = false, Name = "levelClose", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int levelClose
		{
			get
			{
				return this._levelClose;
			}
			set
			{
				this._levelClose = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "taskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskId
		{
			get
			{
				return this._taskId;
			}
			set
			{
				this._taskId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "artifactId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int artifactId
		{
			get
			{
				return this._artifactId;
			}
			set
			{
				this._artifactId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "effect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effect
		{
			get
			{
				return this._effect;
			}
			set
			{
				this._effect = value;
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

		[ProtoMember(9, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "icon2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon2
		{
			get
			{
				return this._icon2;
			}
			set
			{
				this._icon2 = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "bewrite", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bewrite
		{
			get
			{
				return this._bewrite;
			}
			set
			{
				this._bewrite = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "area", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int area
		{
			get
			{
				return this._area;
			}
			set
			{
				this._area = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "areaIndex", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int areaIndex
		{
			get
			{
				return this._areaIndex;
			}
			set
			{
				this._areaIndex = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "widgetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int widgetId
		{
			get
			{
				return this._widgetId;
			}
			set
			{
				this._widgetId = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "notice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int notice
		{
			get
			{
				return this._notice;
			}
			set
			{
				this._notice = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "description", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(17, IsRequired = false, Name = "description2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int description2
		{
			get
			{
				return this._description2;
			}
			set
			{
				this._description2 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
