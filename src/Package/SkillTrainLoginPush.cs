using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2111), ForSend(2111), ProtoContract(Name = "SkillTrainLoginPush")]
	[Serializable]
	public class SkillTrainLoginPush : IExtensible
	{
		public static readonly short OP = 2111;

		private readonly List<SkillTrainInfo> _skills = new List<SkillTrainInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "skills", DataFormat = DataFormat.Default)]
		public List<SkillTrainInfo> skills
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
