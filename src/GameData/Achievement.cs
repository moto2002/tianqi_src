using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Achievement")]
	[Serializable]
	public class Achievement : IExtensible
	{
		private int _id;

		private int _nextId;

		private int _isFrist;

		private int _priority;

		private int _sort;

		private int _linkSystem;

		private readonly List<int> _size = new List<int>();

		private int _dropId;

		private int _name;

		private int _introduction;

		private int _icon;

		private int _schedule;

		private int _go;

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

		[ProtoMember(3, IsRequired = false, Name = "nextId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nextId
		{
			get
			{
				return this._nextId;
			}
			set
			{
				this._nextId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "isFrist", DataFormat = DataFormat.TwosComplement)]
		public int isFrist
		{
			get
			{
				return this._isFrist;
			}
			set
			{
				this._isFrist = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "priority", DataFormat = DataFormat.TwosComplement)]
		public int priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				this._priority = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "sort", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(7, IsRequired = false, Name = "linkSystem", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkSystem
		{
			get
			{
				return this._linkSystem;
			}
			set
			{
				this._linkSystem = value;
			}
		}

		[ProtoMember(8, Name = "size", DataFormat = DataFormat.TwosComplement)]
		public List<int> size
		{
			get
			{
				return this._size;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "dropId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropId
		{
			get
			{
				return this._dropId;
			}
			set
			{
				this._dropId = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "introduction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(12, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "schedule", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(14, IsRequired = false, Name = "go", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int go
		{
			get
			{
				return this._go;
			}
			set
			{
				this._go = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
