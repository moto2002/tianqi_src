using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(474), ForSend(474), ProtoContract(Name = "UseSkillReq")]
	[Serializable]
	public class UseSkillReq : IExtensible
	{
		public static readonly short OP = 474;

		private int _skillId;

		private long _targetId;

		private int _willManaged;

		private Pos _pos;

		private Vector2 _vector;

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

		[ProtoMember(3, IsRequired = true, Name = "willManaged", DataFormat = DataFormat.TwosComplement)]
		public int willManaged
		{
			get
			{
				return this._willManaged;
			}
			set
			{
				this._willManaged = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "pos", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(5, IsRequired = false, Name = "vector", DataFormat = DataFormat.Default), DefaultValue(null)]
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
