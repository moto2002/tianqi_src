using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleAction_UpdateEffect")]
	[Serializable]
	public class BattleAction_UpdateEffect : IExtensible
	{
		[ProtoContract(Name = "ManagedIds")]
		[Serializable]
		public class ManagedIds : IExtensible
		{
			private long _managerId;

			private readonly List<long> _managedIds = new List<long>();

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "managerId", DataFormat = DataFormat.TwosComplement)]
			public long managerId
			{
				get
				{
					return this._managerId;
				}
				set
				{
					this._managerId = value;
				}
			}

			[ProtoMember(2, Name = "managedIds", DataFormat = DataFormat.TwosComplement)]
			public List<long> managedIds
			{
				get
				{
					return this._managedIds;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _effectId;

		private long _uniqueId;

		private long _casterId;

		private readonly List<EffectTargetInfo> _targets = new List<EffectTargetInfo>();

		private readonly List<BattleAction_UpdateEffect.ManagedIds> _needManageTargets = new List<BattleAction_UpdateEffect.ManagedIds>();

		private Pos _pos;

		private Vector2 _vector;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "uniqueId", DataFormat = DataFormat.TwosComplement)]
		public long uniqueId
		{
			get
			{
				return this._uniqueId;
			}
			set
			{
				this._uniqueId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "casterId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, Name = "targets", DataFormat = DataFormat.Default)]
		public List<EffectTargetInfo> targets
		{
			get
			{
				return this._targets;
			}
		}

		[ProtoMember(5, Name = "needManageTargets", DataFormat = DataFormat.Default)]
		public List<BattleAction_UpdateEffect.ManagedIds> needManageTargets
		{
			get
			{
				return this._needManageTargets;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "pos", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Pos pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "vector", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Vector2 vector
		{
			get
			{
				return this._vector;
			}
			set
			{
				this._vector = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
