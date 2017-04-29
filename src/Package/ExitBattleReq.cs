using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(558), ForSend(558), ProtoContract(Name = "ExitBattleReq")]
	[Serializable]
	public class ExitBattleReq : IExtensible
	{
		public static readonly short OP = 558;

		private long _realRoleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "realRoleId", DataFormat = DataFormat.TwosComplement)]
		public long realRoleId
		{
			get
			{
				return this._realRoleId;
			}
			set
			{
				this._realRoleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
