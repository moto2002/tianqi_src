using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3705), ForSend(3705), ProtoContract(Name = "PetUpgradeRes")]
	[Serializable]
	public class PetUpgradeRes : IExtensible
	{
		public static readonly short OP = 3705;

		private PetInfo _petInfo;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
