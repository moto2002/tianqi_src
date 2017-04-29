using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanJiangLi")]
	[Serializable]
	public class JunTuanZhanJiangLi : IExtensible
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

		private int _Ranking;

		private int _PartakeReward;

		private string _Clothes = string.Empty;

		private string _wing = string.Empty;

		private int _Title;

		private readonly List<JunTuanZhanJiangLi.RewardPair> _reward = new List<JunTuanZhanJiangLi.RewardPair>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Ranking", DataFormat = DataFormat.TwosComplement)]
		public int Ranking
		{
			get
			{
				return this._Ranking;
			}
			set
			{
				this._Ranking = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "PartakeReward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int PartakeReward
		{
			get
			{
				return this._PartakeReward;
			}
			set
			{
				this._PartakeReward = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Clothes", DataFormat = DataFormat.Default), DefaultValue("")]
		public string Clothes
		{
			get
			{
				return this._Clothes;
			}
			set
			{
				this._Clothes = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "wing", DataFormat = DataFormat.Default), DefaultValue("")]
		public string wing
		{
			get
			{
				return this._wing;
			}
			set
			{
				this._wing = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "Title", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
			}
		}

		[ProtoMember(7, Name = "reward", DataFormat = DataFormat.Default)]
		public List<JunTuanZhanJiangLi.RewardPair> reward
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
