using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2537), ForSend(2537), ProtoContract(Name = "OpenNpcShopReq")]
	[Serializable]
	public class OpenNpcShopReq : IExtensible
	{
		public static readonly short OP = 2537;

		private int _shopId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "shopId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int shopId
		{
			get
			{
				return this._shopId;
			}
			set
			{
				this._shopId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
