using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShengLiBaoXiang")]
	[Serializable]
	public class ShengLiBaoXiang : IExtensible
	{
		private int _id;

		private int _star;

		private int _dropId1;

		private readonly List<int> _item1 = new List<int>();

		private readonly List<long> _num1 = new List<long>();

		private int _dropId2;

		private readonly List<int> _item2 = new List<int>();

		private readonly List<long> _num2 = new List<long>();

		private int _dropId3;

		private readonly List<int> _item3 = new List<int>();

		private readonly List<long> _num3 = new List<long>();

		private int _dropId4;

		private readonly List<int> _item4 = new List<int>();

		private readonly List<long> _num4 = new List<long>();

		private int _word;

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

		[ProtoMember(3, IsRequired = false, Name = "star", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int star
		{
			get
			{
				return this._star;
			}
			set
			{
				this._star = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "dropId1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropId1
		{
			get
			{
				return this._dropId1;
			}
			set
			{
				this._dropId1 = value;
			}
		}

		[ProtoMember(5, Name = "item1", DataFormat = DataFormat.TwosComplement)]
		public List<int> item1
		{
			get
			{
				return this._item1;
			}
		}

		[ProtoMember(6, Name = "num1", DataFormat = DataFormat.TwosComplement)]
		public List<long> num1
		{
			get
			{
				return this._num1;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "dropId2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropId2
		{
			get
			{
				return this._dropId2;
			}
			set
			{
				this._dropId2 = value;
			}
		}

		[ProtoMember(8, Name = "item2", DataFormat = DataFormat.TwosComplement)]
		public List<int> item2
		{
			get
			{
				return this._item2;
			}
		}

		[ProtoMember(9, Name = "num2", DataFormat = DataFormat.TwosComplement)]
		public List<long> num2
		{
			get
			{
				return this._num2;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "dropId3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropId3
		{
			get
			{
				return this._dropId3;
			}
			set
			{
				this._dropId3 = value;
			}
		}

		[ProtoMember(11, Name = "item3", DataFormat = DataFormat.TwosComplement)]
		public List<int> item3
		{
			get
			{
				return this._item3;
			}
		}

		[ProtoMember(12, Name = "num3", DataFormat = DataFormat.TwosComplement)]
		public List<long> num3
		{
			get
			{
				return this._num3;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "dropId4", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropId4
		{
			get
			{
				return this._dropId4;
			}
			set
			{
				this._dropId4 = value;
			}
		}

		[ProtoMember(14, Name = "item4", DataFormat = DataFormat.TwosComplement)]
		public List<int> item4
		{
			get
			{
				return this._item4;
			}
		}

		[ProtoMember(15, Name = "num4", DataFormat = DataFormat.TwosComplement)]
		public List<long> num4
		{
			get
			{
				return this._num4;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "word", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int word
		{
			get
			{
				return this._word;
			}
			set
			{
				this._word = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
