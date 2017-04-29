using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShangPin")]
	[Serializable]
	public class ShangPin : IExtensible
	{
		private int _Id;

		private int _goodsPool;

		private int _career;

		private int _diamond;

		private int _rmb;

		private int _num;

		private readonly List<int> _discount = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "goodsPool", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goodsPool
		{
			get
			{
				return this._goodsPool;
			}
			set
			{
				this._goodsPool = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "diamond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int diamond
		{
			get
			{
				return this._diamond;
			}
			set
			{
				this._diamond = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "rmb", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rmb
		{
			get
			{
				return this._rmb;
			}
			set
			{
				this._rmb = value;
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

		[ProtoMember(9, Name = "discount", DataFormat = DataFormat.TwosComplement)]
		public List<int> discount
		{
			get
			{
				return this._discount;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
