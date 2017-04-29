using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(681), ForSend(681), ProtoContract(Name = "OffLineLoginPush")]
	[Serializable]
	public class OffLineLoginPush : IExtensible
	{
		public static readonly short OP = 681;

		private int _offTime;

		private int _hasTime;

		private long _addExp;

		private int _roleLv;

		private bool _daily;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "offTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int offTime
		{
			get
			{
				return this._offTime;
			}
			set
			{
				this._offTime = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "hasTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hasTime
		{
			get
			{
				return this._hasTime;
			}
			set
			{
				this._hasTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "addExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long addExp
		{
			get
			{
				return this._addExp;
			}
			set
			{
				this._addExp = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "roleLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int roleLv
		{
			get
			{
				return this._roleLv;
			}
			set
			{
				this._roleLv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "daily", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool daily
		{
			get
			{
				return this._daily;
			}
			set
			{
				this._daily = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
