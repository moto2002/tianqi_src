using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "skillExtend")]
	[Serializable]
	public class skillExtend : IExtensible
	{
		private int _id;

		private int _type;

		private string _ruleDetail = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "ruleDetail", DataFormat = DataFormat.Default), DefaultValue("")]
		public string ruleDetail
		{
			get
			{
				return this._ruleDetail;
			}
			set
			{
				this._ruleDetail = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
