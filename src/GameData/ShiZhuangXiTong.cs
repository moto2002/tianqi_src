using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShiZhuangXiTong")]
	[Serializable]
	public class ShiZhuangXiTong : IExtensible
	{
		private string _ID;

		private int _career;

		private int _itemsID;

		private int _kind;

		private int _quality;

		private int _title;

		private int _gainProperty;

		private int _mallID;

		private int _model;

		private int _IsModelChange;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ID", DataFormat = DataFormat.Default)]
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "itemsID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemsID
		{
			get
			{
				return this._itemsID;
			}
			set
			{
				this._itemsID = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "kind", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._kind = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "quality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "title", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "gainProperty", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "mallID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mallID
		{
			get
			{
				return this._mallID;
			}
			set
			{
				this._mallID = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "IsModelChange", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int IsModelChange
		{
			get
			{
				return this._IsModelChange;
			}
			set
			{
				this._IsModelChange = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
