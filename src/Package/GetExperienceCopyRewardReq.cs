using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(149), ForSend(149), ProtoContract(Name = "GetExperienceCopyRewardReq")]
	[Serializable]
	public class GetExperienceCopyRewardReq : IExtensible
	{
		public static readonly short OP = 149;

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
