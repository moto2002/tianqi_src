using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3677), ForSend(3677), ProtoContract(Name = "UpgradeGuildRes")]
	[Serializable]
	public class UpgradeGuildRes : IExtensible
	{
		public static readonly short OP = 3677;

		private int _guildLv;

		private int _fund;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "guildLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int guildLv
		{
			get
			{
				return this._guildLv;
			}
			set
			{
				this._guildLv = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "fund", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fund
		{
			get
			{
				return this._fund;
			}
			set
			{
				this._fund = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
