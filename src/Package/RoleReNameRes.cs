using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(113), ForSend(113), ProtoContract(Name = "RoleReNameRes")]
	[Serializable]
	public class RoleReNameRes : IExtensible
	{
		public static readonly short OP = 113;

		private string _newName = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "newName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string newName
		{
			get
			{
				return this._newName;
			}
			set
			{
				this._newName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
