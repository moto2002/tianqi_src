using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Package
{
	[ProtoContract(Name = "BattleVertifyInfo")]
	[Serializable]
	public class BattleVertifyInfo : IExtensible
	{
		private readonly List<long> _randIndex = new List<long>();

		private long _casterHp;

		private long _targetHp;

		private string _testStr = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "randIndex", DataFormat = DataFormat.TwosComplement)]
		public List<long> randIndex
		{
			get
			{
				return this._randIndex;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "casterHp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long casterHp
		{
			get
			{
				return this._casterHp;
			}
			set
			{
				this._casterHp = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "targetHp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long targetHp
		{
			get
			{
				return this._targetHp;
			}
			set
			{
				this._targetHp = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "testStr", DataFormat = DataFormat.Default), DefaultValue("")]
		public string testStr
		{
			get
			{
				return this._testStr;
			}
			set
			{
				this._testStr = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[randIndex:(");
			for (int i = 0; i < this.randIndex.get_Count(); i++)
			{
				stringBuilder.Append(this.randIndex.get_Item(i));
				if (i != this.randIndex.get_Count() - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(")");
			stringBuilder.Append(" casterHp: ");
			stringBuilder.Append(this.casterHp);
			stringBuilder.Append(" targetHp: ");
			stringBuilder.Append(this.targetHp);
			stringBuilder.Append(" testStr: ");
			stringBuilder.Append(this.testStr);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
