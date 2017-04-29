using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(570), ForSend(570), ProtoContract(Name = "SignLoginPush")]
	[Serializable]
	public class SignLoginPush : IExtensible
	{
		public static readonly short OP = 570;

		private MonthSignInfo _monthSignInfo;

		private readonly List<MonthTotalInfo> _monthTotalInfo = new List<MonthTotalInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "monthSignInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public MonthSignInfo monthSignInfo
		{
			get
			{
				return this._monthSignInfo;
			}
			set
			{
				this._monthSignInfo = value;
			}
		}

		[ProtoMember(2, Name = "monthTotalInfo", DataFormat = DataFormat.Default)]
		public List<MonthTotalInfo> monthTotalInfo
		{
			get
			{
				return this._monthTotalInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
