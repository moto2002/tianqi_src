using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(66), ForSend(66), ProtoContract(Name = "ChallengeGuildBossRes")]
	[Serializable]
	public class ChallengeGuildBossRes : IExtensible
	{
		public static readonly short OP = 66;

		private int _canKillBossCD;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "canKillBossCD", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int canKillBossCD
		{
			get
			{
				return this._canKillBossCD;
			}
			set
			{
				this._canKillBossCD = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
