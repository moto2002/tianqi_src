using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1312), ForSend(1312), ProtoContract(Name = "BountyRankListRes")]
	[Serializable]
	public class BountyRankListRes : IExtensible
	{
		public static readonly short OP = 1312;

		private BountyRankListInfo _myRankListInfo;

		private readonly List<BountyRankListInfo> _rankListInfo = new List<BountyRankListInfo>();

		private int _nPage;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "myRankListInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public BountyRankListInfo myRankListInfo
		{
			get
			{
				return this._myRankListInfo;
			}
			set
			{
				this._myRankListInfo = value;
			}
		}

		[ProtoMember(2, Name = "rankListInfo", DataFormat = DataFormat.Default)]
		public List<BountyRankListInfo> rankListInfo
		{
			get
			{
				return this._rankListInfo;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "nPage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nPage
		{
			get
			{
				return this._nPage;
			}
			set
			{
				this._nPage = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
