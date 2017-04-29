using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2796), ForSend(2796), ProtoContract(Name = "RoleTalentUpgradeReq")]
	[Serializable]
	public class RoleTalentUpgradeReq : IExtensible
	{
		public static readonly short OP = 2796;

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
