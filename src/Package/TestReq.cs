using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(309), ForSend(309), ProtoContract(Name = "TestReq")]
	[Serializable]
	public class TestReq : IExtensible
	{
		public static readonly short OP = 309;

		private TestType.TT _opType;

		private int _intParam;

		private int _i3;

		private int _i4;

		private int _i5;

		private int _i6;

		private int _i7;

		private int _i8;

		private int _i9;

		private int _i10;

		private string _strParam = string.Empty;

		private string _s2 = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "opType", DataFormat = DataFormat.TwosComplement)]
		public TestType.TT opType
		{
			get
			{
				return this._opType;
			}
			set
			{
				this._opType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "intParam", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int intParam
		{
			get
			{
				return this._intParam;
			}
			set
			{
				this._intParam = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "i3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int i3
		{
			get
			{
				return this._i3;
			}
			set
			{
				this._i3 = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "i4", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int i4
		{
			get
			{
				return this._i4;
			}
			set
			{
				this._i4 = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "i5", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int i5
		{
			get
			{
				return this._i5;
			}
			set
			{
				this._i5 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "i6", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int i6
		{
			get
			{
				return this._i6;
			}
			set
			{
				this._i6 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "i7", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int i7
		{
			get
			{
				return this._i7;
			}
			set
			{
				this._i7 = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "i8", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int i8
		{
			get
			{
				return this._i8;
			}
			set
			{
				this._i8 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "i9", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int i9
		{
			get
			{
				return this._i9;
			}
			set
			{
				this._i9 = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "i10", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int i10
		{
			get
			{
				return this._i10;
			}
			set
			{
				this._i10 = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "strParam", DataFormat = DataFormat.Default), DefaultValue("")]
		public string strParam
		{
			get
			{
				return this._strParam;
			}
			set
			{
				this._strParam = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "s2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string s2
		{
			get
			{
				return this._s2;
			}
			set
			{
				this._s2 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
