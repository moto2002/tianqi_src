using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XXiLianDengJiXiShu")]
	[Serializable]
	public class XXiLianDengJiXiShu : IExtensible
	{
		private int _level;

		private float _levelValue;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "level", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "levelValue", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float levelValue
		{
			get
			{
				return this._levelValue;
			}
			set
			{
				this._levelValue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
