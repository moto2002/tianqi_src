using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Font")]
	[Serializable]
	public class Font : IExtensible
	{
		private int _id;

		private int _font;

		private int _size;

		private int _thickness;

		private readonly List<int> _stroke = new List<int>();

		private int _italic;

		private readonly List<int> _color = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "font", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int font
		{
			get
			{
				return this._font;
			}
			set
			{
				this._font = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "size", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "thickness", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int thickness
		{
			get
			{
				return this._thickness;
			}
			set
			{
				this._thickness = value;
			}
		}

		[ProtoMember(7, Name = "stroke", DataFormat = DataFormat.TwosComplement)]
		public List<int> stroke
		{
			get
			{
				return this._stroke;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "italic", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int italic
		{
			get
			{
				return this._italic;
			}
			set
			{
				this._italic = value;
			}
		}

		[ProtoMember(9, Name = "color", DataFormat = DataFormat.TwosComplement)]
		public List<int> color
		{
			get
			{
				return this._color;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
