using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "SkillConfigInfo")]
	[Serializable]
	public class SkillConfigInfo : IExtensible
	{
		public static readonly short OP = 635;

		private int _skillConfigNum;

		private SkillNotchInfo _notch1;

		private SkillNotchInfo _notch2;

		private SkillNotchInfo _notch3;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillConfigNum", DataFormat = DataFormat.TwosComplement)]
		public int skillConfigNum
		{
			get
			{
				return this._skillConfigNum;
			}
			set
			{
				this._skillConfigNum = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "notch1", DataFormat = DataFormat.Default), DefaultValue(null)]
		public SkillNotchInfo notch1
		{
			get
			{
				return this._notch1;
			}
			set
			{
				this._notch1 = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "notch2", DataFormat = DataFormat.Default), DefaultValue(null)]
		public SkillNotchInfo notch2
		{
			get
			{
				return this._notch2;
			}
			set
			{
				this._notch2 = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "notch3", DataFormat = DataFormat.Default), DefaultValue(null)]
		public SkillNotchInfo notch3
		{
			get
			{
				return this._notch3;
			}
			set
			{
				this._notch3 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
