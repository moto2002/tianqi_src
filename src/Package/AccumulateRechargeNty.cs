using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2483), ForSend(2483), ProtoContract(Name = "AccumulateRechargeNty")]
	[Serializable]
	public class AccumulateRechargeNty : IExtensible
	{
		public static readonly short OP = 2483;

		private readonly List<AccumulateRechargeInfo> _info = new List<AccumulateRechargeInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "info", DataFormat = DataFormat.Default)]
		public List<AccumulateRechargeInfo> info
		{
			get
			{
				return this._info;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
