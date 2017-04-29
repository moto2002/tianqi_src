using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DiscountItemsInfo")]
	[Serializable]
	public class DiscountItemsInfo : IExtensible
	{
		public static readonly short OP = 763;

		private int _id;

		private int _num;

		private float _discount;

		private bool _isOpt;

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

		[ProtoMember(2, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "discount", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float discount
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

		[ProtoMember(4, IsRequired = false, Name = "isOpt", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isOpt
		{
			get
			{
				return this._isOpt;
			}
			set
			{
				this._isOpt = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
