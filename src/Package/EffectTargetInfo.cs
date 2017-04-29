using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "EffectTargetInfo")]
	[Serializable]
	public class EffectTargetInfo : IExtensible
	{
		private long _targetId;

		private bool _knocked;

		private string _hitAction = string.Empty;

		private Pos _toPos;

		private int _curAniPri;

		private int _oldManageState;

		private int _mgrSn;

		private bool _isParry;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "targetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(2, IsRequired = false, Name = "knocked", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool knocked
		{
			get
			{
				return this._knocked;
			}
			set
			{
				this._knocked = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hitAction", DataFormat = DataFormat.Default), DefaultValue("")]
		public string hitAction
		{
			get
			{
				return this._hitAction;
			}
			set
			{
				this._hitAction = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "toPos", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(5, IsRequired = true, Name = "curAniPri", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(6, IsRequired = false, Name = "oldManageState", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "mgrSn", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "isParry", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isParry
		{
			get
			{
				return this._isParry;
			}
			set
			{
				this._isParry = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
