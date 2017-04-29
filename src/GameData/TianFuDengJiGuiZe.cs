using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TianFuDengJiGuiZe")]
	[Serializable]
	public class TianFuDengJiGuiZe : IExtensible
	{
		private string _lvRule;

		private int _minLv;

		private readonly List<int> _item = new List<int>();

		private readonly List<int> _num = new List<int>();

		private int _describe;

		private readonly List<float> _describeValue = new List<float>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "lvRule", DataFormat = DataFormat.Default)]
		public string lvRule
		{
			get
			{
				return this._lvRule;
			}
			set
			{
				this._lvRule = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(5, Name = "item", DataFormat = DataFormat.TwosComplement)]
		public List<int> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(6, Name = "num", DataFormat = DataFormat.TwosComplement)]
		public List<int> num
		{
			get
			{
				return this._num;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "describe", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describe
		{
			get
			{
				return this._describe;
			}
			set
			{
				this._describe = value;
			}
		}

		[ProtoMember(8, Name = "describeValue", DataFormat = DataFormat.FixedSize)]
		public List<float> describeValue
		{
			get
			{
				return this._describeValue;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
