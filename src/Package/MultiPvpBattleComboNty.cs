using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(197), ForSend(197), ProtoContract(Name = "MultiPvpBattleComboNty")]
	[Serializable]
	public class MultiPvpBattleComboNty : IExtensible
	{
		public static readonly short OP = 197;

		private string _roleName = string.Empty;

		private int _combo;

		private IExtension extensionObject;

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

		[ProtoMember(2, IsRequired = false, Name = "combo", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int combo
		{
			get
			{
				return this._combo;
			}
			set
			{
				this._combo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
