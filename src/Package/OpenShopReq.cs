using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7761), ForSend(7761), ProtoContract(Name = "OpenShopReq")]
	[Serializable]
	public class OpenShopReq : IExtensible
	{
		public static readonly short OP = 7761;

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
