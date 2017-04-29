using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(476), ForSend(476), ProtoContract(Name = "UseSkillRes")]
	[Serializable]
	public class UseSkillRes : IExtensible
	{
		public static readonly short OP = 476;

		private int _skillId;

		private long _targetId;

		private int _curAniPri;

		private int _oldManageState;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "skillId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "targetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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
