using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_SuckBlood")]
	[Serializable]
	public class BattleAction_SuckBlood : IExtensible
	{
		private long _soldierId;

		private long _suckHp;

		private long _hp;

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

		[ProtoMember(2, IsRequired = true, Name = "suckHp", DataFormat = DataFormat.TwosComplement)]
		public long suckHp
		{
			get
			{
				return this._suckHp;
			}
			set
			{
				this._suckHp = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "hp", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
