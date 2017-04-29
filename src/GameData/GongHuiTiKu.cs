using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GongHuiTiKu")]
	[Serializable]
	public class GongHuiTiKu : IExtensible
	{
		private int _id;

		private string _question = string.Empty;

		private readonly List<string> _answer = new List<string>();

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

		[ProtoMember(3, IsRequired = false, Name = "question", DataFormat = DataFormat.Default), DefaultValue("")]
		public string question
		{
			get
			{
				return this._question;
			}
			set
			{
				this._question = value;
			}
		}

		[ProtoMember(4, Name = "answer", DataFormat = DataFormat.Default)]
		public List<string> answer
		{
			get
			{
				return this._answer;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
