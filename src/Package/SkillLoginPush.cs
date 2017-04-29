using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(621), ForSend(621), ProtoContract(Name = "SkillLoginPush")]
	[Serializable]
	public class SkillLoginPush : IExtensible
	{
		public static readonly short OP = 621;

		private readonly List<SkillInfo> _skills = new List<SkillInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "skills", DataFormat = DataFormat.Default)]
		public List<SkillInfo> skills
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
