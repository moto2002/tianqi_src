using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(101), ForSend(101), ProtoContract(Name = "ClientCheckTimeRes")]
	[Serializable]
	public class ClientCheckTimeRes : IExtensible
	{
		public static readonly short OP = 101;

		private int _serverTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "serverTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int serverTime
		{
			get
			{
				return this._serverTime;
			}
			set
			{
				this._serverTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
