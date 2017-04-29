using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3821), ForSend(3821), ProtoContract(Name = "CareerAdvancedPush")]
	[Serializable]
	public class CareerAdvancedPush : IExtensible
	{
		public static readonly short OP = 3821;

		private CareerAdvancedInfo _careerAdvancedInfo;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "careerAdvancedInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public CareerAdvancedInfo careerAdvancedInfo
		{
			get
			{
				return this._careerAdvancedInfo;
			}
			set
			{
				this._careerAdvancedInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
