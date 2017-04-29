using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "NPCShangChengBiao")]
	[Serializable]
	public class NPCShangChengBiao : IExtensible
	{
		private int _shopId;

		private int _type;

		private string _startTime = string.Empty;

		private int _RefreshTime;

		private readonly List<int> _itemLibrary = new List<int>();

		private int _itemlimit;

		private readonly List<int> _shopNPC = new List<int>();

		private int _shopBewrite;

		private int _deviation;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "shopId", DataFormat = DataFormat.TwosComplement)]
		public int shopId
		{
			get
			{
				return this._shopId;
			}
			set
			{
				this._shopId = value;
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

		[ProtoMember(4, IsRequired = false, Name = "startTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this._startTime = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "RefreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int RefreshTime
		{
			get
			{
				return this._RefreshTime;
			}
			set
			{
				this._RefreshTime = value;
			}
		}

		[ProtoMember(6, Name = "itemLibrary", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemLibrary
		{
			get
			{
				return this._itemLibrary;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "itemlimit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemlimit
		{
			get
			{
				return this._itemlimit;
			}
			set
			{
				this._itemlimit = value;
			}
		}

		[ProtoMember(8, Name = "shopNPC", DataFormat = DataFormat.TwosComplement)]
		public List<int> shopNPC
		{
			get
			{
				return this._shopNPC;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "shopBewrite", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int shopBewrite
		{
			get
			{
				return this._shopBewrite;
			}
			set
			{
				this._shopBewrite = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "deviation", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deviation
		{
			get
			{
				return this._deviation;
			}
			set
			{
				this._deviation = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
