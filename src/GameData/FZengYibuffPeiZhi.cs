using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "FZengYibuffPeiZhi")]
	[Serializable]
	public class FZengYibuffPeiZhi : IExtensible
	{
		private int _id;

		private int _icon;

		private int _templateId;

		private int _price;

		private int _coinType;

		private int _descId;

		private int _buffName;

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

		[ProtoMember(3, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "templateId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "price", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int price
		{
			get
			{
				return this._price;
			}
			set
			{
				this._price = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "coinType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int coinType
		{
			get
			{
				return this._coinType;
			}
			set
			{
				this._coinType = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "descId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int descId
		{
			get
			{
				return this._descId;
			}
			set
			{
				this._descId = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "buffName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buffName
		{
			get
			{
				return this._buffName;
			}
			set
			{
				this._buffName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
