using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "RenWuLianTiXiTong")]
	[Serializable]
	public class RenWuLianTiXiTong : IExtensible
	{
		[ProtoContract(Name = "KeyidPair")]
		[Serializable]
		public class KeyidPair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
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

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _id;

		private int _minLv;

		private string _picture = string.Empty;

		private int _experience;

		private readonly List<RenWuLianTiXiTong.KeyidPair> _keyId = new List<RenWuLianTiXiTong.KeyidPair>();

		private int _expect;

		private int _least;

		private int _maximum;

		private int _desc;

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

		[ProtoMember(2, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "picture", DataFormat = DataFormat.Default), DefaultValue("")]
		public string picture
		{
			get
			{
				return this._picture;
			}
			set
			{
				this._picture = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "experience", DataFormat = DataFormat.TwosComplement)]
		public int experience
		{
			get
			{
				return this._experience;
			}
			set
			{
				this._experience = value;
			}
		}

		[ProtoMember(5, Name = "keyId", DataFormat = DataFormat.Default)]
		public List<RenWuLianTiXiTong.KeyidPair> keyId
		{
			get
			{
				return this._keyId;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "expect", DataFormat = DataFormat.TwosComplement)]
		public int expect
		{
			get
			{
				return this._expect;
			}
			set
			{
				this._expect = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "least", DataFormat = DataFormat.TwosComplement)]
		public int least
		{
			get
			{
				return this._least;
			}
			set
			{
				this._least = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "maximum", DataFormat = DataFormat.TwosComplement)]
		public int maximum
		{
			get
			{
				return this._maximum;
			}
			set
			{
				this._maximum = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "desc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
