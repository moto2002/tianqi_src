using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1800), ForSend(1800), ProtoContract(Name = "SaveGuideInfoReq")]
	[Serializable]
	public class SaveGuideInfoReq : IExtensible
	{
		public static readonly short OP = 1800;

		private int _guideGroupId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "guideGroupId", DataFormat = DataFormat.TwosComplement)]
		public int guideGroupId
		{
			get
			{
				return this._guideGroupId;
			}
			set
			{
				this._guideGroupId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
