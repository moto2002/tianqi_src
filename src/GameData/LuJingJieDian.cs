using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "LuJingJieDian")]
	[Serializable]
	public class LuJingJieDian : IExtensible
	{
		private int _mapId;

		private readonly List<int> _point1 = new List<int>();

		private readonly List<int> _point2 = new List<int>();

		private readonly List<int> _point3 = new List<int>();

		private readonly List<int> _point4 = new List<int>();

		private readonly List<int> _point5 = new List<int>();

		private readonly List<int> _point6 = new List<int>();

		private readonly List<int> _point7 = new List<int>();

		private readonly List<int> _point8 = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "mapId", DataFormat = DataFormat.TwosComplement)]
		public int mapId
		{
			get
			{
				return this._mapId;
			}
			set
			{
				this._mapId = value;
			}
		}

		[ProtoMember(2, Name = "point1", DataFormat = DataFormat.TwosComplement)]
		public List<int> point1
		{
			get
			{
				return this._point1;
			}
		}

		[ProtoMember(3, Name = "point2", DataFormat = DataFormat.TwosComplement)]
		public List<int> point2
		{
			get
			{
				return this._point2;
			}
		}

		[ProtoMember(4, Name = "point3", DataFormat = DataFormat.TwosComplement)]
		public List<int> point3
		{
			get
			{
				return this._point3;
			}
		}

		[ProtoMember(5, Name = "point4", DataFormat = DataFormat.TwosComplement)]
		public List<int> point4
		{
			get
			{
				return this._point4;
			}
		}

		[ProtoMember(6, Name = "point5", DataFormat = DataFormat.TwosComplement)]
		public List<int> point5
		{
			get
			{
				return this._point5;
			}
		}

		[ProtoMember(7, Name = "point6", DataFormat = DataFormat.TwosComplement)]
		public List<int> point6
		{
			get
			{
				return this._point6;
			}
		}

		[ProtoMember(8, Name = "point7", DataFormat = DataFormat.TwosComplement)]
		public List<int> point7
		{
			get
			{
				return this._point7;
			}
		}

		[ProtoMember(9, Name = "point8", DataFormat = DataFormat.TwosComplement)]
		public List<int> point8
		{
			get
			{
				return this._point8;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
