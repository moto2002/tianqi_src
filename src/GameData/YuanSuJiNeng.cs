using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YuanSuJiNeng")]
	[Serializable]
	public class YuanSuJiNeng : IExtensible
	{
		private int _id;

		private int _name;

		private int _type;

		private int _level;

		private int _grade;

		private int _fragmentID;

		private int _num;

		private readonly List<int> _effect = new List<int>();

		private readonly List<int> _item = new List<int>();

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

		[ProtoMember(4, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "grade", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int grade
		{
			get
			{
				return this._grade;
			}
			set
			{
				this._grade = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "fragmentID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fragmentID
		{
			get
			{
				return this._fragmentID;
			}
			set
			{
				this._fragmentID = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(9, Name = "effect", DataFormat = DataFormat.TwosComplement)]
		public List<int> effect
		{
			get
			{
				return this._effect;
			}
		}

		[ProtoMember(10, Name = "item", DataFormat = DataFormat.TwosComplement)]
		public List<int> item
		{
			get
			{
				return this._item;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
