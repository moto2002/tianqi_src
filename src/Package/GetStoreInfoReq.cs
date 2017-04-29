using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(34), ForSend(34), ProtoContract(Name = "GetStoreInfoReq")]
	[Serializable]
	public class GetStoreInfoReq : IExtensible
	{
		public static readonly short OP = 34;

		private int _storeId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "storeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int storeId
		{
			get
			{
				return this._storeId;
			}
			set
			{
				this._storeId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
