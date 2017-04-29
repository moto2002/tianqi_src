using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBHitMouseRankInfo")]
	[Serializable]
	public class DBHitMouseRankInfo : IExtensible
	{
		public static readonly short OP = 708;

		private long _roleId;

		private string _name = string.Empty;

		private int _gotTime;

		private int _score;

		private int _rank;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "roleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(2, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "gotTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gotTime
		{
			get
			{
				return this._gotTime;
			}
			set
			{
				this._gotTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "score", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int score
		{
			get
			{
				return this._score;
			}
			set
			{
				this._score = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "rank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
