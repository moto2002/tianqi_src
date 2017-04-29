using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "SceneAppConnectSuccessNty")]
	[Serializable]
	public class SceneAppConnectSuccessNty : IExtensible
	{
		private string _appName = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "appName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string appName
		{
			get
			{
				return this._appName;
			}
			set
			{
				this._appName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
