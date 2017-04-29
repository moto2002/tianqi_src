using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(38), ForSend(38), ProtoContract(Name = "ExitExperienceCopyReq")]
	[Serializable]
	public class ExitExperienceCopyReq : IExtensible
	{
		public static readonly short OP = 38;

		private bool _doubleReward;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "doubleReward", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool doubleReward
		{
			get
			{
				return this._doubleReward;
			}
			set
			{
				this._doubleReward = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
