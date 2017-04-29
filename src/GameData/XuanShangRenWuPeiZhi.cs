using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XuanShangRenWuPeiZhi")]
	[Serializable]
	public class XuanShangRenWuPeiZhi : IExtensible
	{
		private int _id;

		private int _missionType;

		private int _quality;

		private int _Picture;

		private int _probability;

		private int _lessPoint;

		private int _maxPoint;

		private int _lessLv;

		private string _describe = string.Empty;

		private float _dropRatio;

		private int _upgradeCost;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "missionType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int missionType
		{
			get
			{
				return this._missionType;
			}
			set
			{
				this._missionType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "quality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this._quality = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "Picture", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Picture
		{
			get
			{
				return this._Picture;
			}
			set
			{
				this._Picture = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "lessPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lessPoint
		{
			get
			{
				return this._lessPoint;
			}
			set
			{
				this._lessPoint = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "maxPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxPoint
		{
			get
			{
				return this._maxPoint;
			}
			set
			{
				this._maxPoint = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "lessLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lessLv
		{
			get
			{
				return this._lessLv;
			}
			set
			{
				this._lessLv = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "describe", DataFormat = DataFormat.Default), DefaultValue("")]
		public string describe
		{
			get
			{
				return this._describe;
			}
			set
			{
				this._describe = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "dropRatio", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float dropRatio
		{
			get
			{
				return this._dropRatio;
			}
			set
			{
				this._dropRatio = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "upgradeCost", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int upgradeCost
		{
			get
			{
				return this._upgradeCost;
			}
			set
			{
				this._upgradeCost = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
