using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XinShouLiBao")]
	[Serializable]
	public class XinShouLiBao : IExtensible
	{
		private int _ID;

		private int _Price;

		private int _Picture;

		private int _Chinese;

		private readonly List<int> _DropId = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public int ID
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

		[ProtoMember(3, IsRequired = false, Name = "Price", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				this._Price = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Picture", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Picture
		{
			get
			{
				return this._Picture;
			}
			set
			{
				this._Picture = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "Chinese", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Chinese
		{
			get
			{
				return this._Chinese;
			}
			set
			{
				this._Chinese = value;
			}
		}

		[ProtoMember(6, Name = "DropId", DataFormat = DataFormat.TwosComplement)]
		public List<int> DropId
		{
			get
			{
				return this._DropId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
