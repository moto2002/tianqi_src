using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(638), ForSend(638), ProtoContract(Name = "SkillChangedNty")]
	[Serializable]
	public class SkillChangedNty : IExtensible
	{
		public static readonly short OP = 638;

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
