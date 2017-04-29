using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1013), ForSend(1013), ProtoContract(Name = "ExploreEnergyChangedNty")]
	[Serializable]
	public class ExploreEnergyChangedNty : IExtensible
	{
		public static readonly short OP = 1013;

		private int _exploreEnergy;

		private int _residueRecoverTime;

		private bool _IsNeedReplaceFlag = true;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "exploreEnergy", DataFormat = DataFormat.TwosComplement)]
		public int exploreEnergy
		{
			get
			{
				return this._exploreEnergy;
			}
			set
			{
				this._exploreEnergy = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "residueRecoverTime", DataFormat = DataFormat.TwosComplement)]
		public int residueRecoverTime
		{
			get
			{
				return this._residueRecoverTime;
			}
			set
			{
				this._residueRecoverTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "IsNeedReplaceFlag", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool IsNeedReplaceFlag
		{
			get
			{
				return this._IsNeedReplaceFlag;
			}
			set
			{
				this._IsNeedReplaceFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
