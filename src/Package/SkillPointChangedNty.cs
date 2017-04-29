using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(870), ForSend(870), ProtoContract(Name = "SkillPointChangedNty")]
	[Serializable]
	public class SkillPointChangedNty : IExtensible
	{
		public static readonly short OP = 870;

		private int _petSkillPoint;

		private int _residueRecoverTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petSkillPoint", DataFormat = DataFormat.TwosComplement)]
		public int petSkillPoint
		{
			get
			{
				return this._petSkillPoint;
			}
			set
			{
				this._petSkillPoint = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "residueRecoverTime", DataFormat = DataFormat.TwosComplement)]
		public int residueRecoverTime
		{
			get
			{
				return this._residueRecoverTime;
			}
			set
			{
				this._residueRecoverTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
