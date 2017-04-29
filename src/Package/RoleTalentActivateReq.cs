using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2798), ForSend(2798), ProtoContract(Name = "RoleTalentActivateReq")]
	[Serializable]
	public class RoleTalentActivateReq : IExtensible
	{
		public static readonly short OP = 2798;

		private int _talentCfgId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "talentCfgId", DataFormat = DataFormat.TwosComplement)]
		public int talentCfgId
		{
			get
			{
				return this._talentCfgId;
			}
			set
			{
				this._talentCfgId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
