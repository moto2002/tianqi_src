using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4451), ForSend(4451), ProtoContract(Name = "BuyRes")]
	[Serializable]
	public class BuyRes : IExtensible
	{
		public static readonly short OP = 4451;

		private int _goodsId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "goodsId", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
