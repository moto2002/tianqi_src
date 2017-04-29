using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(5014), ForSend(5014), ProtoContract(Name = "PetTalentTrainRes")]
	[Serializable]
	public class PetTalentTrainRes : IExtensible
	{
		public static readonly short OP = 5014;

		private PetInfo _petInfo;

		private int _skillPoint;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "petInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public PetInfo petInfo
		{
			get
			{
				return this._petInfo;
			}
			set
			{
				this._petInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "skillPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillPoint
		{
			get
			{
				return this._skillPoint;
			}
			set
			{
				this._skillPoint = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
