using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhuanZhiRenWu")]
	[Serializable]
	public class ZhuanZhiRenWu : IExtensible
	{
		private int _Id;

		private int _missionType;

		private readonly List<int> _missionData = new List<int>();

		private string _message = string.Empty;

		private string _message1 = string.Empty;

		private int _uiJump;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "missionType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int missionType
		{
			get
			{
				return this._missionType;
			}
			set
			{
				this._missionType = value;
			}
		}

		[ProtoMember(4, Name = "missionData", DataFormat = DataFormat.TwosComplement)]
		public List<int> missionData
		{
			get
			{
				return this._missionData;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "message", DataFormat = DataFormat.Default), DefaultValue("")]
		public string message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "message1", DataFormat = DataFormat.Default), DefaultValue("")]
		public string message1
		{
			get
			{
				return this._message1;
			}
			set
			{
				this._message1 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "uiJump", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int uiJump
		{
			get
			{
				return this._uiJump;
			}
			set
			{
				this._uiJump = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
