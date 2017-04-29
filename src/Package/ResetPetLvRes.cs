using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(562), ForSend(562), ProtoContract(Name = "ResetPetLvRes")]
	[Serializable]
	public class ResetPetLvRes : IExtensible
	{
		public static readonly short OP = 562;

		private long _petUUId;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

		private PetInfo _petInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petUUId", DataFormat = DataFormat.TwosComplement)]
		public long petUUId
		{
			get
			{
				return this._petUUId;
			}
			set
			{
				this._petUUId = value;
			}
		}

		[ProtoMember(2, Name = "items", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "petInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public PetInfo petInfo
		{
			get
			{
				return this._petInfo;
			}
			set
			{
				this._petInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
