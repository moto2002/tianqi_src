using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2278), ForSend(2278), ProtoContract(Name = "EliteCopyPush")]
	[Serializable]
	public class EliteCopyPush : IExtensible
	{
		public static readonly short OP = 2278;

		private readonly List<MapInfo> _info = new List<MapInfo>();

		private bool _firstChallenge;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "info", DataFormat = DataFormat.Default)]
		public List<MapInfo> info
		{
			get
			{
				return this._info;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "firstChallenge", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool firstChallenge
		{
			get
			{
				return this._firstChallenge;
			}
			set
			{
				this._firstChallenge = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
