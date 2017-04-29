using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2341), ForSend(2341), ProtoContract(Name = "GetChapterAwardDataReq")]
	[Serializable]
	public class GetChapterAwardDataReq : IExtensible
	{
		public static readonly short OP = 2341;

		private int _canReadChapterAwardId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "canReadChapterAwardId", DataFormat = DataFormat.TwosComplement)]
		public int canReadChapterAwardId
		{
			get
			{
				return this._canReadChapterAwardId;
			}
			set
			{
				this._canReadChapterAwardId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
