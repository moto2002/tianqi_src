using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(74), ForSend(74), ProtoContract(Name = "ExitGuildBossBattleRes")]
	[Serializable]
	public class ExitGuildBossBattleRes : IExtensible
	{
		public static readonly short OP = 74;

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
