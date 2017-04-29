using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(294), ForSend(294), ProtoContract(Name = "SignReq")]
	[Serializable]
	public class SignReq : IExtensible
	{
		public static readonly short OP = 294;

		private int _signFlag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "signFlag", DataFormat = DataFormat.TwosComplement)]
		public int signFlag
		{
			get
			{
				return this._signFlag;
			}
			set
			{
				this._signFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
