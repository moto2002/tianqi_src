using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(417), ForSend(417), ProtoContract(Name = "BuyGoldRes")]
	[Serializable]
	public class BuyGoldRes : IExtensible
	{
		public static readonly short OP = 417;

		private int _extPrize = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "extPrize", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int extPrize
		{
			get
			{
				return this._extPrize;
			}
			set
			{
				this._extPrize = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
