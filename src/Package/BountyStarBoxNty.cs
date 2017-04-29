using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(739), ForSend(739), ProtoContract(Name = "BountyStarBoxNty")]
	[Serializable]
	public class BountyStarBoxNty : IExtensible
	{
		public static readonly short OP = 739;

		private int _typeIdDaily;

		private int _hasStarDaily;

		private bool _hasGotDaily;

		private readonly List<int> _hasGotBoxCfgIds = new List<int>();

		private int _hasStar;

		private uint _nextOpenUtc;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeIdDaily", DataFormat = DataFormat.TwosComplement)]
		public int typeIdDaily
		{
			get
			{
				return this._typeIdDaily;
			}
			set
			{
				this._typeIdDaily = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "hasStarDaily", DataFormat = DataFormat.TwosComplement)]
		public int hasStarDaily
		{
			get
			{
				return this._hasStarDaily;
			}
			set
			{
				this._hasStarDaily = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hasGotDaily", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hasGotDaily
		{
			get
			{
				return this._hasGotDaily;
			}
			set
			{
				this._hasGotDaily = value;
			}
		}

		[ProtoMember(4, Name = "hasGotBoxCfgIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> hasGotBoxCfgIds
		{
			get
			{
				return this._hasGotBoxCfgIds;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "hasStar", DataFormat = DataFormat.TwosComplement)]
		public int hasStar
		{
			get
			{
				return this._hasStar;
			}
			set
			{
				this._hasStar = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "nextOpenUtc", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint nextOpenUtc
		{
			get
			{
				return this._nextOpenUtc;
			}
			set
			{
				this._nextOpenUtc = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
