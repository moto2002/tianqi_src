using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(494), ForSend(494), ProtoContract(Name = "FindBuddyRes")]
	[Serializable]
	public class FindBuddyRes : IExtensible
	{
		public static readonly short OP = 494;

		private BuddyInfo _info;

		private BuddyInfoExt _infoExt;

		private readonly List<WearEquipInfo> _equipInfos = new List<WearEquipInfo>();

		private readonly List<BuddyPetFormation> _petFormation = new List<BuddyPetFormation>();

		private readonly List<string> _fashionList = new List<string>();

		private WearWingInfo _wearWing;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "info", DataFormat = DataFormat.Default), DefaultValue(null)]
		public BuddyInfo info
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

		[ProtoMember(2, IsRequired = false, Name = "infoExt", DataFormat = DataFormat.Default), DefaultValue(null)]
		public BuddyInfoExt infoExt
		{
			get
			{
				return this._infoExt;
			}
			set
			{
				this._infoExt = value;
			}
		}

		[ProtoMember(3, Name = "equipInfos", DataFormat = DataFormat.Default)]
		public List<WearEquipInfo> equipInfos
		{
			get
			{
				return this._equipInfos;
			}
		}

		[ProtoMember(4, Name = "petFormation", DataFormat = DataFormat.Default)]
		public List<BuddyPetFormation> petFormation
		{
			get
			{
				return this._petFormation;
			}
		}

		[ProtoMember(5, Name = "fashionList", DataFormat = DataFormat.Default)]
		public List<string> fashionList
		{
			get
			{
				return this._fashionList;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "wearWing", DataFormat = DataFormat.Default), DefaultValue(null)]
		public WearWingInfo wearWing
		{
			get
			{
				return this._wearWing;
			}
			set
			{
				this._wearWing = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
