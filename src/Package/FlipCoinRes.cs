using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(452), ForSend(452), ProtoContract(Name = "FlipCoinRes")]
	[Serializable]
	public class FlipCoinRes : IExtensible
	{
		public static readonly short OP = 452;

		private int _id;

		private bool _result;

		private int _discount;

		private float _countdown;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "result", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "discount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int discount
		{
			get
			{
				return this._discount;
			}
			set
			{
				this._discount = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "countdown", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float countdown
		{
			get
			{
				return this._countdown;
			}
			set
			{
				this._countdown = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
