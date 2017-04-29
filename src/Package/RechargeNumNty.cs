using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2569), ForSend(2569), ProtoContract(Name = "RechargeNumNty")]
	[Serializable]
	public class RechargeNumNty : IExtensible
	{
		public static readonly short OP = 2569;

		private int _rechargeDiamond;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "rechargeDiamond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rechargeDiamond
		{
			get
			{
				return this._rechargeDiamond;
			}
			set
			{
				this._rechargeDiamond = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
