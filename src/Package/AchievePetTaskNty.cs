using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(200), ForSend(200), ProtoContract(Name = "AchievePetTaskNty")]
	[Serializable]
	public class AchievePetTaskNty : IExtensible
	{
		public static readonly short OP = 200;

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
