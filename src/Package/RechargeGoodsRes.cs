using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(146), ForSend(146), ProtoContract(Name = "RechargeGoodsRes")]
	[Serializable]
	public class RechargeGoodsRes : IExtensible
	{
		public static readonly short OP = 146;

		private readonly List<RechargeGoodsInfo> _Info = new List<RechargeGoodsInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "Info", DataFormat = DataFormat.Default)]
		public List<RechargeGoodsInfo> Info
		{
			get
			{
				return this._Info;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
