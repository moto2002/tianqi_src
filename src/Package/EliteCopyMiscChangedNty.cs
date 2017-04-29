using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(8739), ForSend(8739), ProtoContract(Name = "EliteCopyMiscChangedNty")]
	[Serializable]
	public class EliteCopyMiscChangedNty : IExtensible
	{
		public static readonly short OP = 8739;

		private EliteCopyMisc _misc;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "misc", DataFormat = DataFormat.Default), DefaultValue(null)]
		public EliteCopyMisc misc
		{
			get
			{
				return this._misc;
			}
			set
			{
				this._misc = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
