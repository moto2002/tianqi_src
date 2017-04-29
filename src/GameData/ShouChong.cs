using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShouChong")]
	[Serializable]
	public class ShouChong : IExtensible
	{
		private int _id;

		private int _Diamond;

		private readonly List<int> _Reward = new List<int>();

		private int _RMB;

		private string _model = string.Empty;

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

		[ProtoMember(3, IsRequired = false, Name = "Diamond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Diamond
		{
			get
			{
				return this._Diamond;
			}
			set
			{
				this._Diamond = value;
			}
		}

		[ProtoMember(4, Name = "Reward", DataFormat = DataFormat.TwosComplement)]
		public List<int> Reward
		{
			get
			{
				return this._Reward;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "RMB", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int RMB
		{
			get
			{
				return this._RMB;
			}
			set
			{
				this._RMB = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "model", DataFormat = DataFormat.Default), DefaultValue("")]
		public string model
		{
			get
			{
				return this._model;
			}
			set
			{
				this._model = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
