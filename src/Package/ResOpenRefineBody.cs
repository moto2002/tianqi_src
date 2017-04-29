using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(716), ForSend(716), ProtoContract(Name = "ResOpenRefineBody")]
	[Serializable]
	public class ResOpenRefineBody : IExtensible
	{
		public static readonly short OP = 716;

		private int _curExp;

		private int _stage;

		private readonly List<BrightPoint> _brightPoint = new List<BrightPoint>();

		private bool _isMaxStage;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "curExp", DataFormat = DataFormat.ZigZag)]
		public int curExp
		{
			get
			{
				return this._curExp;
			}
			set
			{
				this._curExp = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "stage", DataFormat = DataFormat.ZigZag)]
		public int stage
		{
			get
			{
				return this._stage;
			}
			set
			{
				this._stage = value;
			}
		}

		[ProtoMember(3, Name = "brightPoint", DataFormat = DataFormat.Default)]
		public List<BrightPoint> brightPoint
		{
			get
			{
				return this._brightPoint;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "isMaxStage", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isMaxStage
		{
			get
			{
				return this._isMaxStage;
			}
			set
			{
				this._isMaxStage = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
