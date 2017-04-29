using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4493), ForSend(4493), ProtoContract(Name = "GetBossDropLogReq")]
	[Serializable]
	public class GetBossDropLogReq : IExtensible
	{
		public static readonly short OP = 4493;

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
