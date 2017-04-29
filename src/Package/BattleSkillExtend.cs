using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleSkillExtend")]
	[Serializable]
	public class BattleSkillExtend : IExtensible
	{
		private int _skillType;

		private int _cdOffset;

		private int _actPointOffset;

		private int _existTimeOffset;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillType", DataFormat = DataFormat.TwosComplement)]
		public int skillType
		{
			get
			{
				return this._skillType;
			}
			set
			{
				this._skillType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "cdOffset", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cdOffset
		{
			get
			{
				return this._cdOffset;
			}
			set
			{
				this._cdOffset = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "actPointOffset", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int actPointOffset
		{
			get
			{
				return this._actPointOffset;
			}
			set
			{
				this._actPointOffset = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "existTimeOffset", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int existTimeOffset
		{
			get
			{
				return this._existTimeOffset;
			}
			set
			{
				this._existTimeOffset = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
