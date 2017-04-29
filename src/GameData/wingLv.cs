using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "wingLv")]
	[Serializable]
	public class wingLv : IExtensible
	{
		[ProtoContract(Name = "UpdatePair")]
		[Serializable]
		public class UpdatePair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _id;

		private int _lv;

		private int _nextLv;

		private readonly List<wingLv.UpdatePair> _update = new List<wingLv.UpdatePair>();

		private int _templateId;

		private int _model;

		private int _icon;

		private string _name;

		private int _color;

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

		[ProtoMember(3, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "nextLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nextLv
		{
			get
			{
				return this._nextLv;
			}
			set
			{
				this._nextLv = value;
			}
		}

		[ProtoMember(5, Name = "update", DataFormat = DataFormat.Default)]
		public List<wingLv.UpdatePair> update
		{
			get
			{
				return this._update;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "templateId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int templateId
		{
			get
			{
				return this._templateId;
			}
			set
			{
				this._templateId = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "model", DataFormat = DataFormat.TwosComplement)]
		public int model
		{
			get
			{
				return this._model;
			}
			set
			{
				this._model = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "icon", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(9, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
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

		[ProtoMember(10, IsRequired = false, Name = "color", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
