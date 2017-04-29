using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2797), ForSend(2797), ProtoContract(Name = "RoleTalentActivateRes")]
	[Serializable]
	public class RoleTalentActivateRes : IExtensible
	{
		public static readonly short OP = 2797;

		private RoleTalentInfo _talent;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "talent", DataFormat = DataFormat.Default), DefaultValue(null)]
		public RoleTalentInfo talent
		{
			get
			{
				return this._talent;
			}
			set
			{
				this._talent = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
