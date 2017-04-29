using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleAction_Fit")]
	[Serializable]
	public class BattleAction_Fit : IExtensible
	{
		private long _roleId;

		private long _fitPetId;

		private int _fitModelId;

		private readonly List<BattleSkillInfo> _fitSkills = new List<BattleSkillInfo>();

		private int _durationTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "fitPetId", DataFormat = DataFormat.TwosComplement)]
		public long fitPetId
		{
			get
			{
				return this._fitPetId;
			}
			set
			{
				this._fitPetId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "fitModelId", DataFormat = DataFormat.TwosComplement)]
		public int fitModelId
		{
			get
			{
				return this._fitModelId;
			}
			set
			{
				this._fitModelId = value;
			}
		}

		[ProtoMember(4, Name = "fitSkills", DataFormat = DataFormat.Default)]
		public List<BattleSkillInfo> fitSkills
		{
			get
			{
				return this._fitSkills;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "durationTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int durationTime
		{
			get
			{
				return this._durationTime;
			}
			set
			{
				this._durationTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
