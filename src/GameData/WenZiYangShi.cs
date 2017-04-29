using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "WenZiYangShi")]
	[Serializable]
	public class WenZiYangShi : IExtensible
	{
		private int _id;

		private readonly List<int> _effectsAdd = new List<int>();

		private readonly List<int> _gradientTopColor = new List<int>();

		private readonly List<int> _gradientBottomColor = new List<int>();

		private readonly List<int> _outLineColor = new List<int>();

		private readonly List<int> _outLineWidth = new List<int>();

		private readonly List<int> _shadowColor = new List<int>();

		private readonly List<int> _shadowOffset = new List<int>();

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

		[ProtoMember(2, Name = "effectsAdd", DataFormat = DataFormat.TwosComplement)]
		public List<int> effectsAdd
		{
			get
			{
				return this._effectsAdd;
			}
		}

		[ProtoMember(3, Name = "gradientTopColor", DataFormat = DataFormat.TwosComplement)]
		public List<int> gradientTopColor
		{
			get
			{
				return this._gradientTopColor;
			}
		}

		[ProtoMember(4, Name = "gradientBottomColor", DataFormat = DataFormat.TwosComplement)]
		public List<int> gradientBottomColor
		{
			get
			{
				return this._gradientBottomColor;
			}
		}

		[ProtoMember(5, Name = "outLineColor", DataFormat = DataFormat.TwosComplement)]
		public List<int> outLineColor
		{
			get
			{
				return this._outLineColor;
			}
		}

		[ProtoMember(6, Name = "outLineWidth", DataFormat = DataFormat.TwosComplement)]
		public List<int> outLineWidth
		{
			get
			{
				return this._outLineWidth;
			}
		}

		[ProtoMember(7, Name = "shadowColor", DataFormat = DataFormat.TwosComplement)]
		public List<int> shadowColor
		{
			get
			{
				return this._shadowColor;
			}
		}

		[ProtoMember(8, Name = "shadowOffset", DataFormat = DataFormat.TwosComplement)]
		public List<int> shadowOffset
		{
			get
			{
				return this._shadowOffset;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
