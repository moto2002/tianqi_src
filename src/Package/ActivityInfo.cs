using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ActivityInfo")]
	[Serializable]
	public class ActivityInfo : IExtensible
	{
		private int _typeId;

		private bool _canGetFlag;

		private bool _firstOpen;

		private bool _overdueFlag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "canGetFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool canGetFlag
		{
			get
			{
				return this._canGetFlag;
			}
			set
			{
				this._canGetFlag = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "firstOpen", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool firstOpen
		{
			get
			{
				return this._firstOpen;
			}
			set
			{
				this._firstOpen = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "overdueFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool overdueFlag
		{
			get
			{
				return this._overdueFlag;
			}
			set
			{
				this._overdueFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
