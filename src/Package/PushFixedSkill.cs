using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2309), ForSend(2309), ProtoContract(Name = "PushFixedSkill")]
	[Serializable]
	public class PushFixedSkill : IExtensible
	{
		public static readonly short OP = 2309;

		private readonly List<BattleSkillInfo> _skills = new List<BattleSkillInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "skills", DataFormat = DataFormat.Default)]
		public List<BattleSkillInfo> skills
		{
			get
			{
				return this._skills;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
