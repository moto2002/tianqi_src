using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "SysConfig")]
	[Serializable]
	public class SysConfig : IExtensible
	{
		private int _id;

		private int _android;

		private int _iphone;

		private int _editor;

		private int _android_release;

		private int _iphone_release;

		private int _editor_release;

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

		[ProtoMember(3, IsRequired = false, Name = "android", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int android
		{
			get
			{
				return this._android;
			}
			set
			{
				this._android = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "iphone", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int iphone
		{
			get
			{
				return this._iphone;
			}
			set
			{
				this._iphone = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "editor", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int editor
		{
			get
			{
				return this._editor;
			}
			set
			{
				this._editor = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "android_release", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int android_release
		{
			get
			{
				return this._android_release;
			}
			set
			{
				this._android_release = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "iphone_release", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int iphone_release
		{
			get
			{
				return this._iphone_release;
			}
			set
			{
				this._iphone_release = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "editor_release", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int editor_release
		{
			get
			{
				return this._editor_release;
			}
			set
			{
				this._editor_release = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
