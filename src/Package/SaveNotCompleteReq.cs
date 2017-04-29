using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(783), ForSend(783), ProtoContract(Name = "SaveNotCompleteReq")]
	[Serializable]
	public class SaveNotCompleteReq : IExtensible
	{
		public static readonly short OP = 783;

		private int _guideGroupId;

		private int _guideStep;

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

		[ProtoMember(2, IsRequired = true, Name = "guideStep", DataFormat = DataFormat.TwosComplement)]
		public int guideStep
		{
			get
			{
				return this._guideStep;
			}
			set
			{
				this._guideStep = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
