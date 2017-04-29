using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1074), ForSend(1074), ProtoContract(Name = "GangFightMatchRoleSummary")]
	[Serializable]
	public class GangFightMatchRoleSummary : IExtensible
	{
		[ProtoContract(Name = "PetInfo")]
		[Serializable]
		public class PetInfo : IExtensible
		{
			private int _petId;

			private int _petLv;

			private int _petIdx;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
			public int petId
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

			[ProtoMember(2, IsRequired = false, Name = "petLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int petLv
			{
				get
				{
					return this._petLv;
				}
				set
				{
					this._petLv = value;
				}
			}

			[ProtoMember(3, IsRequired = false, Name = "petIdx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int petIdx
			{
				get
				{
					return this._petIdx;
				}
				set
				{
					this._petIdx = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 1074;

		private long _roleId;

		private string _name;

		private int _lv;

		private long _fighting;

		private int _modelId;

		private int _currHp;

		private int _maxHp;

		private int _combatWin;

		private int _vipLv;

		private CareerType.CT _career;

		private int _selfCurrHp;

		private int _selfMaxHp;

		private readonly List<GangFightMatchRoleSummary.PetInfo> _petInfo = new List<GangFightMatchRoleSummary.PetInfo>();

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

		[ProtoMember(2, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "fighting", DataFormat = DataFormat.TwosComplement)]
		public long fighting
		{
			get
			{
				return this._fighting;
			}
			set
			{
				this._fighting = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "modelId", DataFormat = DataFormat.TwosComplement)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "currHp", DataFormat = DataFormat.TwosComplement)]
		public int currHp
		{
			get
			{
				return this._currHp;
			}
			set
			{
				this._currHp = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "maxHp", DataFormat = DataFormat.TwosComplement)]
		public int maxHp
		{
			get
			{
				return this._maxHp;
			}
			set
			{
				this._maxHp = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "combatWin", DataFormat = DataFormat.TwosComplement)]
		public int combatWin
		{
			get
			{
				return this._combatWin;
			}
			set
			{
				this._combatWin = value;
			}
		}

		[ProtoMember(9, IsRequired = true, Name = "vipLv", DataFormat = DataFormat.TwosComplement)]
		public int vipLv
		{
			get
			{
				return this._vipLv;
			}
			set
			{
				this._vipLv = value;
			}
		}

		[ProtoMember(10, IsRequired = true, Name = "career", DataFormat = DataFormat.TwosComplement)]
		public CareerType.CT career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(11, IsRequired = true, Name = "selfCurrHp", DataFormat = DataFormat.TwosComplement)]
		public int selfCurrHp
		{
			get
			{
				return this._selfCurrHp;
			}
			set
			{
				this._selfCurrHp = value;
			}
		}

		[ProtoMember(12, IsRequired = true, Name = "selfMaxHp", DataFormat = DataFormat.TwosComplement)]
		public int selfMaxHp
		{
			get
			{
				return this._selfMaxHp;
			}
			set
			{
				this._selfMaxHp = value;
			}
		}

		[ProtoMember(13, Name = "petInfo", DataFormat = DataFormat.Default)]
		public List<GangFightMatchRoleSummary.PetInfo> petInfo
		{
			get
			{
				return this._petInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
