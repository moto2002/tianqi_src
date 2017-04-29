using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(136), ForSend(136), ProtoContract(Name = "UseFreeCardReq")]
	[Serializable]
	public class UseFreeCardReq : IExtensible
	{
		public static readonly short OP = 136;

		private int _itemId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
