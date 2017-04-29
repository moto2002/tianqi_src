using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3123), ForSend(3123), ProtoContract(Name = "GetGiftReq")]
	[Serializable]
	public class GetGiftReq : IExtensible
	{
		public static readonly short OP = 3123;

		private string _key;

		private int _channel;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.Default)]
		public string key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "channel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int channel
		{
			get
			{
				return this._channel;
			}
			set
			{
				this._channel = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
