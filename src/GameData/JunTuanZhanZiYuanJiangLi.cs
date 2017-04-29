using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanZiYuanJiangLi")]
	[Serializable]
	public class JunTuanZhanZiYuanJiangLi : IExtensible
	{
		[ProtoContract(Name = "RewardPair")]
		[Serializable]
		public class RewardPair : IExtensible
		{
			private int _key;

			private int _value;

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

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
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

		private int _Id;

		private string _Resource = string.Empty;

		private readonly List<JunTuanZhanZiYuanJiangLi.RewardPair> _reward = new List<JunTuanZhanZiYuanJiangLi.RewardPair>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "Resource", DataFormat = DataFormat.Default), DefaultValue("")]
		public string Resource
		{
			get
			{
				return this._Resource;
			}
			set
			{
				this._Resource = value;
			}
		}

		[ProtoMember(4, Name = "reward", DataFormat = DataFormat.Default)]
		public List<JunTuanZhanZiYuanJiangLi.RewardPair> reward
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
