using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MonthTotalInfo")]
	[Serializable]
	public class MonthTotalInfo : IExtensible
	{
		private int _id;

		private int _flag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "flag", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int flag
		{
			get
			{
				return this._flag;
			}
			set
			{
				this._flag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
