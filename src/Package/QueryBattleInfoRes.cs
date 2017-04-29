using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(790), ForSend(790), ProtoContract(Name = "QueryBattleInfoRes")]
	[Serializable]
	public class QueryBattleInfoRes : IExtensible
	{
		[ProtoContract(Name = "HurtInfo")]
		[Serializable]
		public class HurtInfo : IExtensible
		{
			private long _roleId;

			private long _totalHurt;

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

			[ProtoMember(2, IsRequired = true, Name = "totalHurt", DataFormat = DataFormat.TwosComplement)]
			public long totalHurt
			{
				get
				{
					return this._totalHurt;
				}
				set
				{
					this._totalHurt = value;
				}
			}

			[ProtoMember(3, IsRequired = false, Name = "roleName", DataFormat = DataFormat.Default), DefaultValue("")]
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

		public static readonly short OP = 790;

		private BattleHurtInfoType _hurtInfoType;

		private readonly List<QueryBattleInfoRes.HurtInfo> _info = new List<QueryBattleInfoRes.HurtInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "hurtInfoType", DataFormat = DataFormat.TwosComplement)]
		public BattleHurtInfoType hurtInfoType
		{
			get
			{
				return this._hurtInfoType;
			}
			set
			{
				this._hurtInfoType = value;
			}
		}

		[ProtoMember(2, Name = "info", DataFormat = DataFormat.Default)]
		public List<QueryBattleInfoRes.HurtInfo> info
		{
			get
			{
				return this._info;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
