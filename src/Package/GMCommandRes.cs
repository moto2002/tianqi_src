using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(461), ForSend(461), ProtoContract(Name = "GMCommandRes")]
	[Serializable]
	public class GMCommandRes : IExtensible
	{
		public static readonly short OP = 461;

		private int _sequent;

		private string _content = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sequent", DataFormat = DataFormat.TwosComplement)]
		public int sequent
		{
			get
			{
				return this._sequent;
			}
			set
			{
				this._sequent = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "content", DataFormat = DataFormat.Default), DefaultValue("")]
		public string content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
