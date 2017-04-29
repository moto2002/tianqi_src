using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GuoChangJuQing")]
	[Serializable]
	public class GuoChangJuQing : IExtensible
	{
		private int _id;

		private int _linkInstance;

		private int _position;

		private readonly List<int> _dialogue = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "linkInstance", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkInstance
		{
			get
			{
				return this._linkInstance;
			}
			set
			{
				this._linkInstance = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(4, Name = "dialogue", DataFormat = DataFormat.TwosComplement)]
		public List<int> dialogue
		{
			get
			{
				return this._dialogue;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
