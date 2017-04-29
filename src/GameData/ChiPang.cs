using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChiPang")]
	[Serializable]
	public class ChiPang : IExtensible
	{
		private int _id;

		private int _color;

		private readonly List<int> _type = new List<int>();

		private readonly List<int> _addlevel = new List<int>();

		private readonly List<int> _nun = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "color", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		[ProtoMember(4, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public List<int> type
		{
			get
			{
				return this._type;
			}
		}

		[ProtoMember(5, Name = "addlevel", DataFormat = DataFormat.TwosComplement)]
		public List<int> addlevel
		{
			get
			{
				return this._addlevel;
			}
		}

		[ProtoMember(7, Name = "nun", DataFormat = DataFormat.TwosComplement)]
		public List<int> nun
		{
			get
			{
				return this._nun;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
