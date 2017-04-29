using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(213), ForSend(213), ProtoContract(Name = "PickUpPetTaskRes")]
	[Serializable]
	public class PickUpPetTaskRes : IExtensible
	{
		public static readonly short OP = 213;

		private PetTaskInfo _task;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "task", DataFormat = DataFormat.Default), DefaultValue(null)]
		public PetTaskInfo task
		{
			get
			{
				return this._task;
			}
			set
			{
				this._task = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
