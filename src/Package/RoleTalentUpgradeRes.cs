using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2795), ForSend(2795), ProtoContract(Name = "RoleTalentUpgradeRes")]
	[Serializable]
	public class RoleTalentUpgradeRes : IExtensible
	{
		public static readonly short OP = 2795;

		private RoleTalentInfo _talent;

		private int _itemId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "talent", DataFormat = DataFormat.Default), DefaultValue(null)]
		public RoleTalentInfo talent
		{
			get
			{
				return this._talent;
			}
			set
			{
				this._talent = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
