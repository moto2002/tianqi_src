using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2792), ForSend(2792), ProtoContract(Name = "RoleTalentPointNty")]
	[Serializable]
	public class RoleTalentPointNty : IExtensible
	{
		public static readonly short OP = 2792;

		private int _talentPoints;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "talentPoints", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
