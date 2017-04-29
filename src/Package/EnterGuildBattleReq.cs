using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(93), ForSend(93), ProtoContract(Name = "EnterGuildBattleReq")]
	[Serializable]
	public class EnterGuildBattleReq : IExtensible
	{
		public static readonly short OP = 93;

		private int _battleNo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "battleNo", DataFormat = DataFormat.TwosComplement)]
		public int battleNo
		{
			get
			{
				return this._battleNo;
			}
			set
			{
				this._battleNo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
