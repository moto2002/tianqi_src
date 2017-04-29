using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(437), ForSend(437), ProtoContract(Name = "ChatNotify")]
	[Serializable]
	public class ChatNotify : IExtensible
	{
		public static readonly short OP = 437;

		private readonly List<TalkMsg> _msgs = new List<TalkMsg>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "msgs", DataFormat = DataFormat.Default)]
		public List<TalkMsg> msgs
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
