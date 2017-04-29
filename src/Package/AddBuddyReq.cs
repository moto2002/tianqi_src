using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(431), ForSend(431), ProtoContract(Name = "AddBuddyReq")]
	[Serializable]
	public class AddBuddyReq : IExtensible
	{
		public static readonly short OP = 431;

		private long _id;

		private bool _newBuddy;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "newBuddy", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool newBuddy
		{
			get
			{
				return this._newBuddy;
			}
			set
			{
				this._newBuddy = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
