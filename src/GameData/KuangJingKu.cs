using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "KuangJingKu")]
	[Serializable]
	public class KuangJingKu : IExtensible
	{
		private int _id;

		private string _holdName = string.Empty;

		private int _Model;

		private readonly List<int> _item = new List<int>();

		private readonly List<int> _itemAddTime = new List<int>();

		private int _chapterId;

		private int _probability;

		private readonly List<int> _petStar = new List<int>();

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

		[ProtoMember(4, Name = "item", DataFormat = DataFormat.TwosComplement)]
		public List<int> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(5, Name = "itemAddTime", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemAddTime
		{
			get
			{
				return this._itemAddTime;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "chapterId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterId
		{
			get
			{
				return this._chapterId;
			}
			set
			{
				this._chapterId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, Name = "petStar", DataFormat = DataFormat.TwosComplement)]
		public List<int> petStar
		{
			get
			{
				return this._petStar;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "depictId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
