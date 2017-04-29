using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BossLabelInfo")]
	[Serializable]
	public class BossLabelInfo : IExtensible
	{
		private int _survivalBossNum;

		private int _nextRemainTimeSec = -1;

		private bool _traceFlag;

		private int _labelId;

		private readonly List<Pos> _pos = new List<Pos>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "survivalBossNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int survivalBossNum
		{
			get
			{
				return this._survivalBossNum;
			}
			set
			{
				this._survivalBossNum = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "nextRemainTimeSec", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int nextRemainTimeSec
		{
			get
			{
				return this._nextRemainTimeSec;
			}
			set
			{
				this._nextRemainTimeSec = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "traceFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool traceFlag
		{
			get
			{
				return this._traceFlag;
			}
			set
			{
				this._traceFlag = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "labelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int labelId
		{
			get
			{
				return this._labelId;
			}
			set
			{
				this._labelId = value;
			}
		}

		[ProtoMember(5, Name = "pos", DataFormat = DataFormat.Default)]
		public List<Pos> pos
		{
			get
			{
				return this._pos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
