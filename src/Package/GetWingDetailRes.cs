using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7766), ForSend(7766), ProtoContract(Name = "GetWingDetailRes")]
	[Serializable]
	public class GetWingDetailRes : IExtensible
	{
		public static readonly short OP = 7766;

		private bool _firstGetWingDetail = true;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "firstGetWingDetail", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool firstGetWingDetail
		{
			get
			{
				return this._firstGetWingDetail;
			}
			set
			{
				this._firstGetWingDetail = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
