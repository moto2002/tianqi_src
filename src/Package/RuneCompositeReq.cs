using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3706), ForSend(3706), ProtoContract(Name = "RuneCompositeReq")]
	[Serializable]
	public class RuneCompositeReq : IExtensible
	{
		public static readonly short OP = 3706;

		private int _toItemId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "toItemId", DataFormat = DataFormat.TwosComplement)]
		public int toItemId
		{
			get
			{
				return this._toItemId;
			}
			set
			{
				this._toItemId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
