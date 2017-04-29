using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2539), ForSend(2539), ProtoContract(Name = "ShoppingRes")]
	[Serializable]
	public class ShoppingRes : IExtensible
	{
		public static readonly short OP = 2539;

		private int _goodsId;

		private int _count;

		private int _stock = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "goodsId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goodsId
		{
			get
			{
				return this._goodsId;
			}
			set
			{
				this._goodsId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "stock", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
