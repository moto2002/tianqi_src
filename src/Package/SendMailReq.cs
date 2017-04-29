using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3771), ForSend(3771), ProtoContract(Name = "SendMailReq")]
	[Serializable]
	public class SendMailReq : IExtensible
	{
		public static readonly short OP = 3771;

		private MailInfo _msg;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "msg", DataFormat = DataFormat.Default)]
		public MailInfo msg
		{
			get
			{
				return this._msg;
			}
			set
			{
				this._msg = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
