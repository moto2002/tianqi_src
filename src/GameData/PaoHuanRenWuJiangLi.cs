using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "PaoHuanRenWuJiangLi")]
	[Serializable]
	public class PaoHuanRenWuJiangLi : IExtensible
	{
		[ProtoContract(Name = "RewardPair")]
		[Serializable]
		public class RewardPair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _taskType;

		private int _time;

		private readonly List<PaoHuanRenWuJiangLi.RewardPair> _reward = new List<PaoHuanRenWuJiangLi.RewardPair>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "taskType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskType
		{
			get
			{
				return this._taskType;
			}
			set
			{
				this._taskType = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(4, Name = "reward", DataFormat = DataFormat.Default)]
		public List<PaoHuanRenWuJiangLi.RewardPair> reward
		{
			get
			{
				return this._reward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
