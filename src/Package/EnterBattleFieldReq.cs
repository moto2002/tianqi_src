using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(793), ForSend(793), ProtoContract(Name = "EnterBattleFieldReq")]
	[Serializable]
	public class EnterBattleFieldReq : IExtensible
	{
		public static readonly short OP = 793;

		private long _echoData;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "echoData", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long echoData
		{
			get
			{
				return this._echoData;
			}
			set
			{
				this._echoData = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
