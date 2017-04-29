using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "SShiZhuang")]
	[Serializable]
	public class SShiZhuang : IExtensible
	{
		private int _Id;

		private int _itemId;

		private int _career;

		private int _Price2;

		private int _Price3;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "Price2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Price2
		{
			get
			{
				return this._Price2;
			}
			set
			{
				this._Price2 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "Price3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Price3
		{
			get
			{
				return this._Price3;
			}
			set
			{
				this._Price3 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
