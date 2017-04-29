using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2799), ForSend(2799), ProtoContract(Name = "RoleTalentInfoPush")]
	[Serializable]
	public class RoleTalentInfoPush : IExtensible
	{
		public static readonly short OP = 2799;

		private readonly List<RoleTalentInfo> _talents = new List<RoleTalentInfo>();

		private int _talentPoints;

		private int _resetTalentCount;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "talents", DataFormat = DataFormat.Default)]
		public List<RoleTalentInfo> talents
		{
			get
			{
				return this._talents;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "talentPoints", DataFormat = DataFormat.TwosComplement)]
		public int talentPoints
		{
			get
			{
				return this._talentPoints;
			}
			set
			{
				this._talentPoints = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "resetTalentCount", DataFormat = DataFormat.TwosComplement)]
		public int resetTalentCount
		{
			get
			{
				return this._resetTalentCount;
			}
			set
			{
				this._resetTalentCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
