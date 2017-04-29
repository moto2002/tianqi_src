using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(174), ForSend(174), ProtoContract(Name = "GetGuildSkillRes")]
	[Serializable]
	public class GetGuildSkillRes : IExtensible
	{
		public static readonly short OP = 174;

		private readonly List<GuildSkillInfo> _skillInfo = new List<GuildSkillInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "skillInfo", DataFormat = DataFormat.Default)]
		public List<GuildSkillInfo> skillInfo
		{
			get
			{
				return this._skillInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
