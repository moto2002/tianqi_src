using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(422), ForSend(422), ProtoContract(Name = "BuyFundReq")]
	[Serializable]
	public class BuyFundReq : IExtensible
	{
		public static readonly short OP = 422;

		private int _typeId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
