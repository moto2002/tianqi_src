using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1295), ForSend(1295), ProtoContract(Name = "InstantlyAckReq")]
	[Serializable]
	public class InstantlyAckReq : IExtensible
	{
		public static readonly short OP = 1295;

		private byte[] _argBuf;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "argBuf", DataFormat = DataFormat.Default)]
		public byte[] argBuf
		{
			get
			{
				return this._argBuf;
			}
			set
			{
				this._argBuf = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
