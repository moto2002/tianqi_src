using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GuanLiGongNeng")]
	[Serializable]
	public class GuanLiGongNeng : IExtensible
	{
		private int _function;

		private int _chinese;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "function", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int function
		{
			get
			{
				return this._function;
			}
			set
			{
				this._function = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "chinese", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chinese
		{
			get
			{
				return this._chinese;
			}
			set
			{
				this._chinese = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
