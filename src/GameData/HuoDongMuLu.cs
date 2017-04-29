using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "HuoDongMuLu")]
	[Serializable]
	public class HuoDongMuLu : IExtensible
	{
		private int _type;

		private int _nameId;

		private int _priority;

		private int _icon;

		private int _openLv;

		private int _descId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "nameId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nameId
		{
			get
			{
				return this._nameId;
			}
			set
			{
				this._nameId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "priority", DataFormat = DataFormat.TwosComplement)]
		public int priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				this._priority = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "openLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openLv
		{
			get
			{
				return this._openLv;
			}
			set
			{
				this._openLv = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "descId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int descId
		{
			get
			{
				return this._descId;
			}
			set
			{
				this._descId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
