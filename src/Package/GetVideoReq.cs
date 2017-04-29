using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1332), ForSend(1332), ProtoContract(Name = "GetVideoReq")]
	[Serializable]
	public class GetVideoReq : IExtensible
	{
		public static readonly short OP = 1332;

		private long _videoUId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "videoUId", DataFormat = DataFormat.TwosComplement)]
		public long videoUId
		{
			get
			{
				return this._videoUId;
			}
			set
			{
				this._videoUId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
