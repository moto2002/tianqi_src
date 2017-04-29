using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6811), ForSend(6811), ProtoContract(Name = "BuyDiscountItemRes")]
	[Serializable]
	public class BuyDiscountItemRes : IExtensible
	{
		public static readonly short OP = 6811;

		private int _id;

		private int _nums;

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

		[ProtoMember(2, IsRequired = false, Name = "nums", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nums
		{
			get
			{
				return this._nums;
			}
			set
			{
				this._nums = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
