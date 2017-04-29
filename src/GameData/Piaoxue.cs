using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Piaoxue")]
	[Serializable]
	public class Piaoxue : IExtensible
	{
		private int _id;

		private int _target;

		private int _distance;

		private int _damageType;

		private readonly List<int> _color = new List<int>();

		private int _track;

		private int _resource;

		private readonly List<int> _picSize = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "target", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "distance", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				this._distance = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "damageType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int damageType
		{
			get
			{
				return this._damageType;
			}
			set
			{
				this._damageType = value;
			}
		}

		[ProtoMember(6, Name = "color", DataFormat = DataFormat.TwosComplement)]
		public List<int> color
		{
			get
			{
				return this._color;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "track", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int track
		{
			get
			{
				return this._track;
			}
			set
			{
				this._track = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "resource", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resource
		{
			get
			{
				return this._resource;
			}
			set
			{
				this._resource = value;
			}
		}

		[ProtoMember(9, Name = "picSize", DataFormat = DataFormat.TwosComplement)]
		public List<int> picSize
		{
			get
			{
				return this._picSize;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
