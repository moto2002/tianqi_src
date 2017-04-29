using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "vipTiYanQia")]
	[Serializable]
	public class vipTiYanQia : IExtensible
	{
		private int _ID;

		private int _icon;

		private readonly List<int> _effect = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, Name = "effect", DataFormat = DataFormat.TwosComplement)]
		public List<int> effect
		{
			get
			{
				return this._effect;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
