using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "MailSendType")]
	[Serializable]
	public class MailSendType : IExtensible
	{
		[ProtoContract(Name = "MST")]
		public enum MST
		{
			[ProtoEnum(Name = "Broadcast", Value = 0)]
			Broadcast,
			[ProtoEnum(Name = "Multicast", Value = 1)]
			Multicast
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
