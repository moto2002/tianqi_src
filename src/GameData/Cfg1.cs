using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Cfg1")]
	[Serializable]
	public class Cfg1 : IExtensible
	{
		[ProtoContract(Name = "ThedictPair")]
		[Serializable]
		public class ThedictPair : IExtensible
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

		private int _val1;

		private long _val2;

		private int _indexedVal;

		private bool _boolVal = true;

		private float _floatVal;

		private readonly List<string> _text = new List<string>();

		private readonly List<Cfg1.ThedictPair> _theDict = new List<Cfg1.ThedictPair>();

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = false, Name = "val1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int val1
		{
			get
			{
				return this._val1;
			}
			set
			{
				this._val1 = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "val2", DataFormat = DataFormat.TwosComplement)]
		public long val2
		{
			get
			{
				return this._val2;
			}
			set
			{
				this._val2 = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "indexedVal", DataFormat = DataFormat.TwosComplement)]
		public int indexedVal
		{
			get
			{
				return this._indexedVal;
			}
			set
			{
				this._indexedVal = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "boolVal", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool boolVal
		{
			get
			{
				return this._boolVal;
			}
			set
			{
				this._boolVal = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "floatVal", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float floatVal
		{
			get
			{
				return this._floatVal;
			}
			set
			{
				this._floatVal = value;
			}
		}

		[ProtoMember(9, Name = "text", DataFormat = DataFormat.Default)]
		public List<string> text
		{
			get
			{
				return this._text;
			}
		}

		[ProtoMember(10, Name = "theDict", DataFormat = DataFormat.Default)]
		public List<Cfg1.ThedictPair> theDict
		{
			get
			{
				return this._theDict;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
