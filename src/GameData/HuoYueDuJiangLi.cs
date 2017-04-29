using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "HuoYueDuJiangLi")]
	[Serializable]
	public class HuoYueDuJiangLi : IExtensible
	{
		private int _id;

		private int _numericalValue;

		private readonly List<int> _reward = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "numericalValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int numericalValue
		{
			get
			{
				return this._numericalValue;
			}
			set
			{
				this._numericalValue = value;
			}
		}

		[ProtoMember(4, Name = "reward", DataFormat = DataFormat.TwosComplement)]
		public List<int> reward
		{
			get
			{
				return this._reward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
