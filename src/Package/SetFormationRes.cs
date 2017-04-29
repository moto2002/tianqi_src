using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(671), ForSend(671), ProtoContract(Name = "SetFormationRes")]
	[Serializable]
	public class SetFormationRes : IExtensible
	{
		public static readonly short OP = 671;

		private PetFormation _formation;

		private bool _firstFlag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "formation", DataFormat = DataFormat.Default)]
		public PetFormation formation
		{
			get
			{
				return this._formation;
			}
			set
			{
				this._formation = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "firstFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool firstFlag
		{
			get
			{
				return this._firstFlag;
			}
			set
			{
				this._firstFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
