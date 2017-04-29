using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2249), ForSend(2249), ProtoContract(Name = "BuySkillConfigReq")]
	[Serializable]
	public class BuySkillConfigReq : IExtensible
	{
		public static readonly short OP = 2249;

		private int _skillConfigNum;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillConfigNum", DataFormat = DataFormat.TwosComplement)]
		public int skillConfigNum
		{
			get
			{
				return this._skillConfigNum;
			}
			set
			{
				this._skillConfigNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
