using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(584), ForSend(584), ProtoContract(Name = "UpdateTokenAck")]
	[Serializable]
	public class UpdateTokenAck : IExtensible
	{
		public static readonly short OP = 584;

		private string _token;

		private int _seq;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "token", DataFormat = DataFormat.Default)]
		public string token
		{
			get
			{
				return this._token;
			}
			set
			{
				this._token = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "seq", DataFormat = DataFormat.TwosComplement)]
		public int seq
		{
			get
			{
				return this._seq;
			}
			set
			{
				this._seq = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
