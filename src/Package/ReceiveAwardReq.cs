using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1236), ForSend(1236), ProtoContract(Name = "ReceiveAwardReq")]
	[Serializable]
	public class ReceiveAwardReq : IExtensible
	{
		public static readonly short OP = 1236;

		private int _chapterAwardId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "chapterAwardId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterAwardId
		{
			get
			{
				return this._chapterAwardId;
			}
			set
			{
				this._chapterAwardId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
