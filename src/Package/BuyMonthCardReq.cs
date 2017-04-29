using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2762), ForSend(2762), ProtoContract(Name = "BuyMonthCardReq")]
	[Serializable]
	public class BuyMonthCardReq : IExtensible
	{
		public static readonly short OP = 2762;

		private int _id;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
