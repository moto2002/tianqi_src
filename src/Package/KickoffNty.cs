using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4075), ForSend(4075), ProtoContract(Name = "KickoffNty")]
	[Serializable]
	public class KickoffNty : IExtensible
	{
		public static readonly short OP = 4075;

		private int _teamId;

		private TeamDetailReason _reason;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int teamId
		{
			get
			{
				return this._teamId;
			}
			set
			{
				this._teamId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "reason", DataFormat = DataFormat.Default), DefaultValue(null)]
		public TeamDetailReason reason
		{
			get
			{
				return this._reason;
			}
			set
			{
				this._reason = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
