using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleAction_AddBuff")]
	[Serializable]
	public class BattleAction_AddBuff : IExtensible
	{
		private long _casterId;

		private long _targetId;

		private int _buffId;

		private int _dueTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "casterId", DataFormat = DataFormat.TwosComplement)]
		public long casterId
		{
			get
			{
				return this._casterId;
			}
			set
			{
				this._casterId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "targetId", DataFormat = DataFormat.TwosComplement)]
		public long targetId
		{
			get
			{
				return this._targetId;
			}
			set
			{
				this._targetId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "buffId", DataFormat = DataFormat.TwosComplement)]
		public int buffId
		{
			get
			{
				return this._buffId;
			}
			set
			{
				this._buffId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "dueTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dueTime
		{
			get
			{
				return this._dueTime;
			}
			set
			{
				this._dueTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
