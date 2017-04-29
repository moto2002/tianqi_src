using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MapObjDecorations")]
	[Serializable]
	public class MapObjDecorations : IExtensible
	{
		private readonly List<int> _equipIds = new List<int>();

		private long _petUUId;

		private int _petId;

		private int _petStar;

		private int _wingId;

		private bool _wingHidden = true;

		private int _career;

		private int _wingLv;

		private readonly List<string> _fashions = new List<string>();

		private int _modelId;

		private int _gogokNum;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "equipIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> equipIds
		{
			get
			{
				return this._equipIds;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "petUUId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long petUUId
		{
			get
			{
				return this._petUUId;
			}
			set
			{
				this._petUUId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "petId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petId
		{
			get
			{
				return this._petId;
			}
			set
			{
				this._petId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "petStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petStar
		{
			get
			{
				return this._petStar;
			}
			set
			{
				this._petStar = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "wingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int wingId
		{
			get
			{
				return this._wingId;
			}
			set
			{
				this._wingId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "wingHidden", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool wingHidden
		{
			get
			{
				return this._wingHidden;
			}
			set
			{
				this._wingHidden = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "wingLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int wingLv
		{
			get
			{
				return this._wingLv;
			}
			set
			{
				this._wingLv = value;
			}
		}

		[ProtoMember(9, Name = "fashions", DataFormat = DataFormat.Default)]
		public List<string> fashions
		{
			get
			{
				return this._fashions;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "modelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "gogokNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gogokNum
		{
			get
			{
				return this._gogokNum;
			}
			set
			{
				this._gogokNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
