using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleAction")]
	[Serializable]
	public class BattleAction : IExtensible
	{
		private int _actType;

		private int _actIndex;

		private byte[] _argBuf;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "actType", DataFormat = DataFormat.TwosComplement)]
		public int actType
		{
			get
			{
				return this._actType;
			}
			set
			{
				this._actType = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "actIndex", DataFormat = DataFormat.TwosComplement)]
		public int actIndex
		{
			get
			{
				return this._actIndex;
			}
			set
			{
				this._actIndex = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "argBuf", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] argBuf
		{
			get
			{
				return this._argBuf;
			}
			set
			{
				this._argBuf = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
