using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3701), ForSend(3701), ProtoContract(Name = "ComposePetRes")]
	[Serializable]
	public class ComposePetRes : IExtensible
	{
		public static readonly short OP = 3701;

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
