using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(730), ForSend(730), ProtoContract(Name = "PassiveSkillPush")]
	[Serializable]
	public class PassiveSkillPush : IExtensible
	{
		public static readonly short OP = 730;

		private readonly List<PassiveSkillInfo> _skill = new List<PassiveSkillInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "skill", DataFormat = DataFormat.Default)]
		public List<PassiveSkillInfo> skill
		{
			get
			{
				return this._skill;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
