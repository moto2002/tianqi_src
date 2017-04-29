using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(721), ForSend(721), ProtoContract(Name = "PvePlayerExitNty")]
	[Serializable]
	public class PvePlayerExitNty : IExtensible
	{
		[ProtoContract(Name = "RoleInfo")]
		[Serializable]
		public class RoleInfo : IExtensible
		{
			private long _roleId;

			private string _roleName = string.Empty;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
			public long roleId
			{
				get
				{
					return this._roleId;
				}
				set
				{
					this._roleId = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "roleName", DataFormat = DataFormat.Default), DefaultValue("")]
			public string roleName
			{
				get
				{
					return this._roleName;
				}
				set
				{
					this._roleName = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 721;

		private readonly List<PvePlayerExitNty.RoleInfo> _roles = new List<PvePlayerExitNty.RoleInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "roles", DataFormat = DataFormat.Default)]
		public List<PvePlayerExitNty.RoleInfo> roles
		{
			get
			{
				return this._roles;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
