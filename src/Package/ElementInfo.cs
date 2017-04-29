using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ElementInfo")]
	[Serializable]
	public class ElementInfo : IExtensible
	{
		private int _elemId;

		private int _elemLv;

		private int _elemMaxLv;

		private bool _upgradable;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "elemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int elemId
		{
			get
			{
				return this._elemId;
			}
			set
			{
				this._elemId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "elemLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int elemLv
		{
			get
			{
				return this._elemLv;
			}
			set
			{
				this._elemLv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "elemMaxLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int elemMaxLv
		{
			get
			{
				return this._elemMaxLv;
			}
			set
			{
				this._elemMaxLv = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "upgradable", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool upgradable
		{
			get
			{
				return this._upgradable;
			}
			set
			{
				this._upgradable = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
