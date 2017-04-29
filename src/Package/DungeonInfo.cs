using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DungeonInfo")]
	[Serializable]
	public class DungeonInfo : IExtensible
	{
		private int _dungeonId;

		private bool _clearance;

		private int _star;

		private int _remainingChallengeTimes;

		private int _resetChallengeTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dungeonId", DataFormat = DataFormat.TwosComplement)]
		public int dungeonId
		{
			get
			{
				return this._dungeonId;
			}
			set
			{
				this._dungeonId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "clearance", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool clearance
		{
			get
			{
				return this._clearance;
			}
			set
			{
				this._clearance = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "star", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int star
		{
			get
			{
				return this._star;
			}
			set
			{
				this._star = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "remainingChallengeTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainingChallengeTimes
		{
			get
			{
				return this._remainingChallengeTimes;
			}
			set
			{
				this._remainingChallengeTimes = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "resetChallengeTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resetChallengeTimes
		{
			get
			{
				return this._resetChallengeTimes;
			}
			set
			{
				this._resetChallengeTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
