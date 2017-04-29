using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(328), ForSend(328), ProtoContract(Name = "TestNty")]
	[Serializable]
	public class TestNty : IExtensible
	{
		public static readonly short OP = 328;

		private TestType.TT _opType;

		private int _intParam;

		private string _strParam = string.Empty;

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

		[ProtoMember(3, IsRequired = false, Name = "strParam", DataFormat = DataFormat.Default), DefaultValue("")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
