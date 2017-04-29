using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChengHao")]
	[Serializable]
	public class ChengHao : IExtensible
	{
		private int _id;

		private int _belong;

		private int _quality;

		private int _displayWay;

		private int _icon;

		private int _condition;

		private int _size;

		private int _schedule;

		private int _introduction;

		private int _duration;

		private int _gainProperty;

		private int _gainIntroduction;

		private int _sort;

		private int _nameId;

		private int _type;

		private int _replaceable;

		private int _superposition;

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

		[ProtoMember(3, IsRequired = true, Name = "belong", DataFormat = DataFormat.TwosComplement)]
		public int belong
		{
			get
			{
				return this._belong;
			}
			set
			{
				this._belong = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "quality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this._quality = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "displayWay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int displayWay
		{
			get
			{
				return this._displayWay;
			}
			set
			{
				this._displayWay = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "size", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "schedule", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int schedule
		{
			get
			{
				return this._schedule;
			}
			set
			{
				this._schedule = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "introduction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "duration", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._duration = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "gainProperty", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gainProperty
		{
			get
			{
				return this._gainProperty;
			}
			set
			{
				this._gainProperty = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "gainIntroduction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gainIntroduction
		{
			get
			{
				return this._gainIntroduction;
			}
			set
			{
				this._gainIntroduction = value;
			}
		}

		[ProtoMember(14, IsRequired = true, Name = "sort", DataFormat = DataFormat.TwosComplement)]
		public int sort
		{
			get
			{
				return this._sort;
			}
			set
			{
				this._sort = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "nameId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nameId
		{
			get
			{
				return this._nameId;
			}
			set
			{
				this._nameId = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(17, IsRequired = false, Name = "replaceable", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int replaceable
		{
			get
			{
				return this._replaceable;
			}
			set
			{
				this._replaceable = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "superposition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int superposition
		{
			get
			{
				return this._superposition;
			}
			set
			{
				this._superposition = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
