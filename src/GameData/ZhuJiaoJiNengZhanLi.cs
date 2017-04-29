using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhuJiaoJiNengZhanLi")]
	[Serializable]
	public class ZhuJiaoJiNengZhanLi : IExtensible
	{
		private int _groupId;

		private int _skillId;

		private int _lv;

		private int _FightPower;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "groupId", DataFormat = DataFormat.TwosComplement)]
		public int groupId
		{
			get
			{
				return this._groupId;
			}
			set
			{
				this._groupId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "skillId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "FightPower", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int FightPower
		{
			get
			{
				return this._FightPower;
			}
			set
			{
				this._FightPower = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
