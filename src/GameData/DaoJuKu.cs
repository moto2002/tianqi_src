using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DaoJuKu")]
	[Serializable]
	public class DaoJuKu : IExtensible
	{
		private int _id;

		private string _holdName = string.Empty;

		private int _Model;

		private int _dropId;

		private int _probability;

		private int _depictId;

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

		[ProtoMember(2, IsRequired = false, Name = "holdName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string holdName
		{
			get
			{
				return this._holdName;
			}
			set
			{
				this._holdName = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "Model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Model
		{
			get
			{
				return this._Model;
			}
			set
			{
				this._Model = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "dropId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropId
		{
			get
			{
				return this._dropId;
			}
			set
			{
				this._dropId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "depictId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int depictId
		{
			get
			{
				return this._depictId;
			}
			set
			{
				this._depictId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
