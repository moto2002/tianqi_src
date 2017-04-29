using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(182), ForSend(182), ProtoContract(Name = "EliteChallengeFirstRes")]
	[Serializable]
	public class EliteChallengeFirstRes : IExtensible
	{
		public static readonly short OP = 182;

		private bool _firstChallenge;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "firstChallenge", DataFormat = DataFormat.Default), DefaultValue(false)]
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
