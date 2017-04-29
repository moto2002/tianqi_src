using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3904), ForSend(3904), ProtoContract(Name = "GetBossPageInfoReq")]
	[Serializable]
	public class GetBossPageInfoReq : IExtensible
	{
		public static readonly short OP = 3904;

		private int _pageId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "pageId", DataFormat = DataFormat.TwosComplement)]
		public int pageId
		{
			get
			{
				return this._pageId;
			}
			set
			{
				this._pageId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
