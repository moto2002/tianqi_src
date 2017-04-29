using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "RewardInfo")]
	[Serializable]
	public class RewardInfo : IExtensible
	{
		private int _Id;

		private bool _canGet;

		private bool _hadGet;

		private bool _overdue;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "Id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "canGet", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool canGet
		{
			get
			{
				return this._canGet;
			}
			set
			{
				this._canGet = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hadGet", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hadGet
		{
			get
			{
				return this._hadGet;
			}
			set
			{
				this._hadGet = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "overdue", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool overdue
		{
			get
			{
				return this._overdue;
			}
			set
			{
				this._overdue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
