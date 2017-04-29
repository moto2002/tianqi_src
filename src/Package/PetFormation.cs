using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "PetFormation")]
	[Serializable]
	public class PetFormation : IExtensible
	{
		private int _formationId;

		private Int64ArrayMsg _petFormationArr;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "formationId", DataFormat = DataFormat.TwosComplement)]
		public int formationId
		{
			get
			{
				return this._formationId;
			}
			set
			{
				this._formationId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "petFormationArr", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Int64ArrayMsg petFormationArr
		{
			get
			{
				return this._petFormationArr;
			}
			set
			{
				this._petFormationArr = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
