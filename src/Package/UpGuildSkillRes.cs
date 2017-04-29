using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(176), ForSend(176), ProtoContract(Name = "UpGuildSkillRes")]
	[Serializable]
	public class UpGuildSkillRes : IExtensible
	{
		public static readonly short OP = 176;

		private int _skillId;

		private int _skillLv;

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

		[ProtoMember(2, IsRequired = false, Name = "skillLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillLv
		{
			get
			{
				return this._skillLv;
			}
			set
			{
				this._skillLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
