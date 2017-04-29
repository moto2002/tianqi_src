using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(198), ForSend(198), ProtoContract(Name = "GetPetTaskRewardRes")]
	[Serializable]
	public class GetPetTaskRewardRes : IExtensible
	{
		public static readonly short OP = 198;

		private long _idx;

		private PetTaskInfo _task;

		private bool _success;

		private readonly List<ItemBriefInfo> _rewards = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "idx", DataFormat = DataFormat.TwosComplement)]
		public long idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "task", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(3, IsRequired = false, Name = "success", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool success
		{
			get
			{
				return this._success;
			}
			set
			{
				this._success = value;
			}
		}

		[ProtoMember(4, Name = "rewards", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> rewards
		{
			get
			{
				return this._rewards;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
