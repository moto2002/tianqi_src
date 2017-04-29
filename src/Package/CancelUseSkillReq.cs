using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(707), ForSend(707), ProtoContract(Name = "CancelUseSkillReq")]
	[Serializable]
	public class CancelUseSkillReq : IExtensible
	{
		public static readonly short OP = 707;

		private int _skillId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
