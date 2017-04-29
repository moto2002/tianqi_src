using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MonthSignInfo")]
	[Serializable]
	public class MonthSignInfo : IExtensible
	{
		private bool _isSign;

		private int _signDays;

		private int _repairNum;

		private int _repairUsedNum;

		private readonly List<int> _serialDays = new List<int>();

		private int _year;

		private int _month;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "isSign", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isSign
		{
			get
			{
				return this._isSign;
			}
			set
			{
				this._isSign = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "signDays", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int signDays
		{
			get
			{
				return this._signDays;
			}
			set
			{
				this._signDays = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "repairNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int repairNum
		{
			get
			{
				return this._repairNum;
			}
			set
			{
				this._repairNum = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "repairUsedNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int repairUsedNum
		{
			get
			{
				return this._repairUsedNum;
			}
			set
			{
				this._repairUsedNum = value;
			}
		}

		[ProtoMember(5, Name = "serialDays", DataFormat = DataFormat.TwosComplement)]
		public List<int> serialDays
		{
			get
			{
				return this._serialDays;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "year", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int year
		{
			get
			{
				return this._year;
			}
			set
			{
				this._year = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "month", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int month
		{
			get
			{
				return this._month;
			}
			set
			{
				this._month = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
