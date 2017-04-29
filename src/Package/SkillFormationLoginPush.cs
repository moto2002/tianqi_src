using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(5666), ForSend(5666), ProtoContract(Name = "SkillFormationLoginPush")]
	[Serializable]
	public class SkillFormationLoginPush : IExtensible
	{
		public static readonly short OP = 5666;

		private readonly List<SetSkillFormation> _infos = new List<SetSkillFormation>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infos", DataFormat = DataFormat.Default)]
		public List<SetSkillFormation> infos
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
