using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "LuckDrawInfo")]
	[Serializable]
	public class LuckDrawInfo : IExtensible
	{
		private int _drawId;

		private bool _open;

		private int _drawedTimes;

		private readonly List<DrawResultInfo> _resultInfos = new List<DrawResultInfo>();

		private int _refreshTime = -1;

		private int _hadTimes;

		private int _diamondTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "drawId", DataFormat = DataFormat.TwosComplement)]
		public int drawId
		{
			get
			{
				return this._drawId;
			}
			set
			{
				this._drawId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "open", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool open
		{
			get
			{
				return this._open;
			}
			set
			{
				this._open = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "drawedTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int drawedTimes
		{
			get
			{
				return this._drawedTimes;
			}
			set
			{
				this._drawedTimes = value;
			}
		}

		[ProtoMember(4, Name = "resultInfos", DataFormat = DataFormat.Default)]
		public List<DrawResultInfo> resultInfos
		{
			get
			{
				return this._resultInfos;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "refreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int refreshTime
		{
			get
			{
				return this._refreshTime;
			}
			set
			{
				this._refreshTime = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "hadTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hadTimes
		{
			get
			{
				return this._hadTimes;
			}
			set
			{
				this._hadTimes = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "diamondTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int diamondTimes
		{
			get
			{
				return this._diamondTimes;
			}
			set
			{
				this._diamondTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
