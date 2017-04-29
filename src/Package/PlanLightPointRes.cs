using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(736), ForSend(736), ProtoContract(Name = "PlanLightPointRes")]
	[Serializable]
	public class PlanLightPointRes : IExtensible
	{
		public static readonly short OP = 736;

		private int _plantLightPoint;

		private bool _isLastPoint;

		private int _linePointId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "plantLightPoint", DataFormat = DataFormat.ZigZag), DefaultValue(0)]
		public int plantLightPoint
		{
			get
			{
				return this._plantLightPoint;
			}
			set
			{
				this._plantLightPoint = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "isLastPoint", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isLastPoint
		{
			get
			{
				return this._isLastPoint;
			}
			set
			{
				this._isLastPoint = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "linePointId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linePointId
		{
			get
			{
				return this._linePointId;
			}
			set
			{
				this._linePointId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
