using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DetailInfo")]
	[Serializable]
	public class DetailInfo : IExtensible
	{
		private DetailType.DT _type;

		private string _label;

		private int _placeholder;

		private byte[] _audio;

		private long _id;

		private int _cfgId;

		private int _icon;

		private long _num;

		private long _mailId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public DetailType.DT type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "label", DataFormat = DataFormat.Default)]
		public string label
		{
			get
			{
				return this._label;
			}
			set
			{
				this._label = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "placeholder", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int placeholder
		{
			get
			{
				return this._placeholder;
			}
			set
			{
				this._placeholder = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "audio", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] audio
		{
			get
			{
				return this._audio;
			}
			set
			{
				this._audio = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long id
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

		[ProtoMember(6, IsRequired = false, Name = "cfgId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cfgId
		{
			get
			{
				return this._cfgId;
			}
			set
			{
				this._cfgId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "mailId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long mailId
		{
			get
			{
				return this._mailId;
			}
			set
			{
				this._mailId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
