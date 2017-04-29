using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(440), ForSend(440), ProtoContract(Name = "MailNotify")]
	[Serializable]
	public class MailNotify : IExtensible
	{
		public static readonly short OP = 440;

		private readonly List<MailInfo> _msgs = new List<MailInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "msgs", DataFormat = DataFormat.Default)]
		public List<MailInfo> msgs
		{
			get
			{
				return this._msgs;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
