using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(869), ForSend(869), ProtoContract(Name = "OnLoginChatServerNty")]
	[Serializable]
	public class OnLoginChatServerNty : IExtensible
	{
		public static readonly short OP = 869;

		private long _roleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "roleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
