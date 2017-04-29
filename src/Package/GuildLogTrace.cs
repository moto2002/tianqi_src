using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GuildLogTrace")]
	[Serializable]
	public class GuildLogTrace : IExtensible
	{
		[ProtoContract(Name = "ItemInfo")]
		[Serializable]
		public class ItemInfo : IExtensible
		{
			private int _itemId;

			private int _itemCount;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
			public int itemId
			{
				get
				{
					return this._itemId;
				}
				set
				{
					this._itemId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "itemCount", DataFormat = DataFormat.TwosComplement)]
			public int itemCount
			{
				get
				{
					return this._itemCount;
				}
				set
				{
					this._itemCount = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private GuildLogType.GDLT _logType;

		private string _roleName = string.Empty;

		private int _logTimeUtc;

		private int _value;

		private readonly List<GuildLogTrace.ItemInfo> _items = new List<GuildLogTrace.ItemInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "logType", DataFormat = DataFormat.TwosComplement)]
		public GuildLogType.GDLT logType
		{
			get
			{
				return this._logType;
			}
			set
			{
				this._logType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "roleName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string roleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "logTimeUtc", DataFormat = DataFormat.TwosComplement)]
		public int logTimeUtc
		{
			get
			{
				return this._logTimeUtc;
			}
			set
			{
				this._logTimeUtc = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		[ProtoMember(5, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuildLogTrace.ItemInfo> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
