using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(201), ForSend(201), ProtoContract(Name = "SurvivalChallengeResultNty")]
	[Serializable]
	public class SurvivalChallengeResultNty : IExtensible
	{
		public static readonly short OP = 201;

		private bool _win;

		private SurvivalChallengeGetBaseInfoNty _info;

		private ChallengeResult _result;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "win", DataFormat = DataFormat.Default)]
		public bool win
		{
			get
			{
				return this._win;
			}
			set
			{
				this._win = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "info", DataFormat = DataFormat.Default), DefaultValue(null)]
		public SurvivalChallengeGetBaseInfoNty info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "result", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ChallengeResult result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
