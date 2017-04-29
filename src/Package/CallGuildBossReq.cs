using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(70), ForSend(70), ProtoContract(Name = "CallGuildBossReq")]
	[Serializable]
	public class CallGuildBossReq : IExtensible
	{
		public static readonly short OP = 70;

		private int _bossId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "bossId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossId
		{
			get
			{
				return this._bossId;
			}
			set
			{
				this._bossId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
