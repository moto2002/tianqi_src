using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "BattleAction_ExitFit")]
	[Serializable]
	public class BattleAction_ExitFit : IExtensible
	{
		private long _roleId;

		private long _petId;

		private int _roleModelId;

		private readonly List<BattleSkillInfo> _roleSkills = new List<BattleSkillInfo>();

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

		[ProtoMember(2, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public long petId
		{
			get
			{
				return this._petId;
			}
			set
			{
				this._petId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "roleModelId", DataFormat = DataFormat.TwosComplement)]
		public int roleModelId
		{
			get
			{
				return this._roleModelId;
			}
			set
			{
				this._roleModelId = value;
			}
		}

		[ProtoMember(4, Name = "roleSkills", DataFormat = DataFormat.Default)]
		public List<BattleSkillInfo> roleSkills
		{
			get
			{
				return this._roleSkills;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
