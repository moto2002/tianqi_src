using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(904), ForSend(904), ProtoContract(Name = "GuildStorageChangeNty")]
	[Serializable]
	public class GuildStorageChangeNty : IExtensible
	{
		public static readonly short OP = 904;

		private GuildStorageInfo _storageInfos;

		private PersonalInfo _personalInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "storageInfos", DataFormat = DataFormat.Default), DefaultValue(null)]
		public GuildStorageInfo storageInfos
		{
			get
			{
				return this._storageInfos;
			}
			set
			{
				this._storageInfos = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "personalInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public PersonalInfo personalInfo
		{
			get
			{
				return this._personalInfo;
			}
			set
			{
				this._personalInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
