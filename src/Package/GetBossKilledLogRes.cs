using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4497), ForSend(4497), ProtoContract(Name = "GetBossKilledLogRes")]
	[Serializable]
	public class GetBossKilledLogRes : IExtensible
	{
		public static readonly short OP = 4497;

		private readonly List<BossKilledLog> _bossKilledLog = new List<BossKilledLog>();

		private int _labelId;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "bossKilledLog", DataFormat = DataFormat.Default)]
		public List<BossKilledLog> bossKilledLog
		{
			get
			{
				return this._bossKilledLog;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "labelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int labelId
		{
			get
			{
				return this._labelId;
			}
			set
			{
				this._labelId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
