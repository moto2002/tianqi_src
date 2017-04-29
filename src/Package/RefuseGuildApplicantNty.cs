using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1021), ForSend(1021), ProtoContract(Name = "RefuseGuildApplicantNty")]
	[Serializable]
	public class RefuseGuildApplicantNty : IExtensible
	{
		public static readonly short OP = 1021;

		private string _reason = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "reason", DataFormat = DataFormat.Default), DefaultValue("")]
		public string reason
		{
			get
			{
				return this._reason;
			}
			set
			{
				this._reason = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
