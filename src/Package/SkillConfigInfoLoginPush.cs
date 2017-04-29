using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1035), ForSend(1035), ProtoContract(Name = "SkillConfigInfoLoginPush")]
	[Serializable]
	public class SkillConfigInfoLoginPush : IExtensible
	{
		public static readonly short OP = 1035;

		private readonly List<SkillConfigInfo> _infos = new List<SkillConfigInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infos", DataFormat = DataFormat.Default)]
		public List<SkillConfigInfo> infos
		{
			get
			{
				return this._infos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
