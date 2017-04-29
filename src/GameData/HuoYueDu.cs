using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "HuoYueDu")]
	[Serializable]
	public class HuoYueDu : IExtensible
	{
		private int _id;

		private int _numericalValue;

		private int _reward;

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

		[ProtoMember(4, IsRequired = false, Name = "reward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reward
		{
			get
			{
				return this._reward;
			}
			set
			{
				this._reward = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
