using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XuanJiaoPeiZhi")]
	[Serializable]
	public class XuanJiaoPeiZhi : IExtensible
	{
		[ProtoContract(Name = "CapabilityPair")]
		[Serializable]
		public class CapabilityPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private string _professionId;

		private string _modelId = string.Empty;

		private int _professionName;

		private int _introduction;

		private int _rotationAngle;

		private readonly List<int> _cameraPos = new List<int>();

		private readonly List<int> _cameraLookPos = new List<int>();

		private int _icon;

		private int _iconHightLight;

		private readonly List<XuanJiaoPeiZhi.CapabilityPair> _capability = new List<XuanJiaoPeiZhi.CapabilityPair>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "professionId", DataFormat = DataFormat.Default)]
		public string professionId
		{
			get
			{
				return this._professionId;
			}
			set
			{
				this._professionId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "modelId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string modelId
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

		[ProtoMember(6, IsRequired = false, Name = "professionName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int professionName
		{
			get
			{
				return this._professionName;
			}
			set
			{
				this._professionName = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "introduction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int introduction
		{
			get
			{
				return this._introduction;
			}
			set
			{
				this._introduction = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "rotationAngle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rotationAngle
		{
			get
			{
				return this._rotationAngle;
			}
			set
			{
				this._rotationAngle = value;
			}
		}

		[ProtoMember(9, Name = "cameraPos", DataFormat = DataFormat.TwosComplement)]
		public List<int> cameraPos
		{
			get
			{
				return this._cameraPos;
			}
		}

		[ProtoMember(10, Name = "cameraLookPos", DataFormat = DataFormat.TwosComplement)]
		public List<int> cameraLookPos
		{
			get
			{
				return this._cameraLookPos;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "iconHightLight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int iconHightLight
		{
			get
			{
				return this._iconHightLight;
			}
			set
			{
				this._iconHightLight = value;
			}
		}

		[ProtoMember(13, Name = "capability", DataFormat = DataFormat.Default)]
		public List<XuanJiaoPeiZhi.CapabilityPair> capability
		{
			get
			{
				return this._capability;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
