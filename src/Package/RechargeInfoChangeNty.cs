using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2321), ForSend(2321), ProtoContract(Name = "RechargeInfoChangeNty")]
	[Serializable]
	public class RechargeInfoChangeNty : IExtensible
	{
		public static readonly short OP = 2321;

		private readonly List<RechargeInfo> _rechargeInfos = new List<RechargeInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "rechargeInfos", DataFormat = DataFormat.Default)]
		public List<RechargeInfo> rechargeInfos
		{
			get
			{
				return this._rechargeInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
