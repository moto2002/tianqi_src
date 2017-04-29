using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_LegalizeHp")]
	[Serializable]
	public class BattleAction_LegalizeHp : IExtensible
	{
		public static readonly short OP = 994;

		private long _soldierId;

		private long _hp;

		private long _hpLmt;

		private int _hpLmtMulAmend;

		private int _hpLmtAddAmend;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "soldierId", DataFormat = DataFormat.TwosComplement)]
		public long soldierId
		{
			get
			{
				return this._soldierId;
			}
			set
			{
				this._soldierId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "hp", DataFormat = DataFormat.TwosComplement)]
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this._hp = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "hpLmt", DataFormat = DataFormat.TwosComplement)]
		public long hpLmt
		{
			get
			{
				return this._hpLmt;
			}
			set
			{
				this._hpLmt = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "hpLmtMulAmend", DataFormat = DataFormat.TwosComplement)]
		public int hpLmtMulAmend
		{
			get
			{
				return this._hpLmtMulAmend;
			}
			set
			{
				this._hpLmtMulAmend = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "hpLmtAddAmend", DataFormat = DataFormat.TwosComplement)]
		public int hpLmtAddAmend
		{
			get
			{
				return this._hpLmtAddAmend;
			}
			set
			{
				this._hpLmtAddAmend = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
