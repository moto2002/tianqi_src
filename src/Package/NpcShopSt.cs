using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "NpcShopSt")]
	[Serializable]
	public class NpcShopSt : IExtensible
	{
		private int _shopId;

		private int _nextUpdateTime = -1;

		private bool _sellOut;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "shopId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int shopId
		{
			get
			{
				return this._shopId;
			}
			set
			{
				this._shopId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "nextUpdateTime", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int nextUpdateTime
		{
			get
			{
				return this._nextUpdateTime;
			}
			set
			{
				this._nextUpdateTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "sellOut", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool sellOut
		{
			get
			{
				return this._sellOut;
			}
			set
			{
				this._sellOut = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
