using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2319), ForSend(2319), ProtoContract(Name = "RechargeInfosLoginPush")]
	[Serializable]
	public class RechargeInfosLoginPush : IExtensible
	{
		public static readonly short OP = 2319;

		private readonly List<RechargeInfo> _rechargeInfos = new List<RechargeInfo>();

		private int _rechargeDiamond;

		private readonly List<AccumulateRechargeInfo> _info = new List<AccumulateRechargeInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "rechargeInfos", DataFormat = DataFormat.Default)]
		public List<RechargeInfo> rechargeInfos
		{
			get
			{
				return this._rechargeInfos;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "rechargeDiamond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, Name = "info", DataFormat = DataFormat.Default)]
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
