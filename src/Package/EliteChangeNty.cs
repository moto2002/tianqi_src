using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2271), ForSend(2271), ProtoContract(Name = "EliteChangeNty")]
	[Serializable]
	public class EliteChangeNty : IExtensible
	{
		public static readonly short OP = 2271;

		private EliteCopyInfo _info;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "info", DataFormat = DataFormat.Default), DefaultValue(null)]
		public EliteCopyInfo info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
