using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleAction_Assault")]
	[Serializable]
	public class BattleAction_Assault : IExtensible
	{
		private long _soldierId;

		private Pos _toPos;

		private int _curAniPri;

		private int _oldManageState;

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

		[ProtoMember(2, IsRequired = true, Name = "toPos", DataFormat = DataFormat.Default)]
		public Pos toPos
		{
			get
			{
				return this._toPos;
			}
			set
			{
				this._toPos = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "curAniPri", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int curAniPri
		{
			get
			{
				return this._curAniPri;
			}
			set
			{
				this._curAniPri = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "oldManageState", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oldManageState
		{
			get
			{
				return this._oldManageState;
			}
			set
			{
				this._oldManageState = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
