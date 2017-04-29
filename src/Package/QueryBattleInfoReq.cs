using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(788), ForSend(788), ProtoContract(Name = "QueryBattleInfoReq")]
	[Serializable]
	public class QueryBattleInfoReq : IExtensible
	{
		public static readonly short OP = 788;

		private int _needRoleName;

		private BattleHurtInfoType _hurtInfoType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "needRoleName", DataFormat = DataFormat.TwosComplement)]
		public int needRoleName
		{
			get
			{
				return this._needRoleName;
			}
			set
			{
				this._needRoleName = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "hurtInfoType", DataFormat = DataFormat.TwosComplement)]
		public BattleHurtInfoType hurtInfoType
		{
			get
			{
				return this._hurtInfoType;
			}
			set
			{
				this._hurtInfoType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
