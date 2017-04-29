using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleAction_UseSkill")]
	[Serializable]
	public class BattleAction_UseSkill : IExtensible
	{
		private int _skillId;

		private long _casterId;

		private Vector2 _casterVector;

		private long _targetId;

		private bool _needManage;

		private bool _isManaged;

		private int _curAniPri;

		private int _oldManageState;

		private int _mgrSn;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "casterId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = true, Name = "casterVector", DataFormat = DataFormat.Default)]
		public Vector2 casterVector
		{
			get
			{
				return this._casterVector;
			}
			set
			{
				this._casterVector = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "targetId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = false, Name = "needManage", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool needManage
		{
			get
			{
				return this._needManage;
			}
			set
			{
				this._needManage = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "isManaged", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isManaged
		{
			get
			{
				return this._isManaged;
			}
			set
			{
				this._isManaged = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "curAniPri", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(8, IsRequired = false, Name = "oldManageState", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "mgrSn", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mgrSn
		{
			get
			{
				return this._mgrSn;
			}
			set
			{
				this._mgrSn = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
