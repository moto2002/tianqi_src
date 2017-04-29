using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "RawInfo")]
	[Serializable]
	public class RawInfo : IExtensible
	{
		private int _acId;

		private Tab.TAB _tab;

		private SubTab.ST _subTab;

		private long _servletId;

		private long _chineseId;

		private readonly List<int> _needParams = new List<int>();

		private ItemInfo1 _rewardItem;

		private int _startDay;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "acId", DataFormat = DataFormat.TwosComplement)]
		public int acId
		{
			get
			{
				return this._acId;
			}
			set
			{
				this._acId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "tab", DataFormat = DataFormat.TwosComplement)]
		public Tab.TAB tab
		{
			get
			{
				return this._tab;
			}
			set
			{
				this._tab = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "subTab", DataFormat = DataFormat.TwosComplement)]
		public SubTab.ST subTab
		{
			get
			{
				return this._subTab;
			}
			set
			{
				this._subTab = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "servletId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long servletId
		{
			get
			{
				return this._servletId;
			}
			set
			{
				this._servletId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "chineseId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long chineseId
		{
			get
			{
				return this._chineseId;
			}
			set
			{
				this._chineseId = value;
			}
		}

		[ProtoMember(6, Name = "needParams", DataFormat = DataFormat.TwosComplement)]
		public List<int> needParams
		{
			get
			{
				return this._needParams;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "rewardItem", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ItemInfo1 rewardItem
		{
			get
			{
				return this._rewardItem;
			}
			set
			{
				this._rewardItem = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "startDay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int startDay
		{
			get
			{
				return this._startDay;
			}
			set
			{
				this._startDay = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
