using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "NPCShangPinKu")]
	[Serializable]
	public class NPCShangPinKu : IExtensible
	{
		private int _Id;

		private int _libraryId;

		private int _itemId;

		private readonly List<int> _materialLibraryID = new List<int>();

		private int _itemType;

		private int _num;

		private int _stock;

		private int _reputation;

		private int _weight;

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

		[ProtoMember(3, IsRequired = false, Name = "libraryId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int libraryId
		{
			get
			{
				return this._libraryId;
			}
			set
			{
				this._libraryId = value;
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

		[ProtoMember(5, Name = "materialLibraryID", DataFormat = DataFormat.TwosComplement)]
		public List<int> materialLibraryID
		{
			get
			{
				return this._materialLibraryID;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "itemType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				this._itemType = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "stock", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int stock
		{
			get
			{
				return this._stock;
			}
			set
			{
				this._stock = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "reputation", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reputation
		{
			get
			{
				return this._reputation;
			}
			set
			{
				this._reputation = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
