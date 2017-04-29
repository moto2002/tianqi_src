using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GuangBoLianJie")]
	[Serializable]
	public class GuangBoLianJie : IExtensible
	{
		private int _hitEventId;

		private int _type;

		private int _name;

		private int _link;

		private int _interface;

		private int _click;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "hitEventId", DataFormat = DataFormat.TwosComplement)]
		public int hitEventId
		{
			get
			{
				return this._hitEventId;
			}
			set
			{
				this._hitEventId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
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

		[ProtoMember(4, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "link", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int link
		{
			get
			{
				return this._link;
			}
			set
			{
				this._link = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "interface", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int @interface
		{
			get
			{
				return this._interface;
			}
			set
			{
				this._interface = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "click", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int click
		{
			get
			{
				return this._click;
			}
			set
			{
				this._click = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
