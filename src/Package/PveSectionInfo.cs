using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "PveSectionInfo")]
	[Serializable]
	public class PveSectionInfo : IExtensible
	{
		private int _sectionId;

		private bool _isPassed;

		private int _todayRemainWinTimes;

		private int _star;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sectionId", DataFormat = DataFormat.TwosComplement)]
		public int sectionId
		{
			get
			{
				return this._sectionId;
			}
			set
			{
				this._sectionId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "isPassed", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isPassed
		{
			get
			{
				return this._isPassed;
			}
			set
			{
				this._isPassed = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "todayRemainWinTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int todayRemainWinTimes
		{
			get
			{
				return this._todayRemainWinTimes;
			}
			set
			{
				this._todayRemainWinTimes = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "star", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int star
		{
			get
			{
				return this._star;
			}
			set
			{
				this._star = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
