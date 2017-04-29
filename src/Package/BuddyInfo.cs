using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BuddyInfo")]
	[Serializable]
	public class BuddyInfo : IExtensible
	{
		private long _id;

		private string _name;

		private int _career;

		private int _lv;

		private long _fighting;

		private int _vipLv;

		private bool _online;

		private BuddyRelation.BR _relation = BuddyRelation.BR.Stranger;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
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

		[ProtoMember(2, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "career", DataFormat = DataFormat.TwosComplement)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "fighting", DataFormat = DataFormat.TwosComplement)]
		public long fighting
		{
			get
			{
				return this._fighting;
			}
			set
			{
				this._fighting = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "vipLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vipLv
		{
			get
			{
				return this._vipLv;
			}
			set
			{
				this._vipLv = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "online", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool online
		{
			get
			{
				return this._online;
			}
			set
			{
				this._online = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "relation", DataFormat = DataFormat.TwosComplement), DefaultValue(BuddyRelation.BR.Stranger)]
		public BuddyRelation.BR relation
		{
			get
			{
				return this._relation;
			}
			set
			{
				this._relation = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
