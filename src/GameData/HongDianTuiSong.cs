using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "HongDianTuiSong")]
	[Serializable]
	public class HongDianTuiSong : IExtensible
	{
		private int _type;

		private int _name;

		private int _push;

		private int _lv;

		private int _jump;

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

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "push", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int push
		{
			get
			{
				return this._push;
			}
			set
			{
				this._push = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "jump", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int jump
		{
			get
			{
				return this._jump;
			}
			set
			{
				this._jump = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
