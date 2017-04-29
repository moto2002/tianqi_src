using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(450), ForSend(450), ProtoContract(Name = "FlipCoinReq")]
	[Serializable]
	public class FlipCoinReq : IExtensible
	{
		public static readonly short OP = 450;

		private int _id;

		private bool _side;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
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

		[ProtoMember(2, IsRequired = false, Name = "side", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool side
		{
			get
			{
				return this._side;
			}
			set
			{
				this._side = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
