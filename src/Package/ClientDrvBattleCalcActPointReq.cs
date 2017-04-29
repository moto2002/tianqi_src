using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1245), ForSend(1245), ProtoContract(Name = "ClientDrvBattleCalcActPointReq")]
	[Serializable]
	public class ClientDrvBattleCalcActPointReq : IExtensible
	{
		public static readonly short OP = 1245;

		private long _fromId;

		private long _toId;

		private int _skillId;

		private long _realId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "fromId", DataFormat = DataFormat.TwosComplement)]
		public long fromId
		{
			get
			{
				return this._fromId;
			}
			set
			{
				this._fromId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "toId", DataFormat = DataFormat.TwosComplement)]
		public long toId
		{
			get
			{
				return this._toId;
			}
			set
			{
				this._toId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "realId", DataFormat = DataFormat.TwosComplement)]
		public long realId
		{
			get
			{
				return this._realId;
			}
			set
			{
				this._realId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
