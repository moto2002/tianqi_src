using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DiaoLuoGuiZe")]
	[Serializable]
	public class DiaoLuoGuiZe : IExtensible
	{
		private int _id;

		private int _ruleId;

		private int _ruletype;

		private int _groupId;

		private int _first;

		private string _beginTime = string.Empty;

		private string _endTime = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "ruleId", DataFormat = DataFormat.TwosComplement)]
		public int ruleId
		{
			get
			{
				return this._ruleId;
			}
			set
			{
				this._ruleId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "ruletype", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ruletype
		{
			get
			{
				return this._ruletype;
			}
			set
			{
				this._ruletype = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "groupId", DataFormat = DataFormat.TwosComplement)]
		public int groupId
		{
			get
			{
				return this._groupId;
			}
			set
			{
				this._groupId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "first", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int first
		{
			get
			{
				return this._first;
			}
			set
			{
				this._first = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "beginTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string beginTime
		{
			get
			{
				return this._beginTime;
			}
			set
			{
				this._beginTime = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "endTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
