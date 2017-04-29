using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(293), ForSend(293), ProtoContract(Name = "DrawRes")]
	[Serializable]
	public class DrawRes : IExtensible
	{
		public static readonly short OP = 293;

		private readonly List<AwardInfo> _awardInfos = new List<AwardInfo>();

		private int _resId;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "awardInfos", DataFormat = DataFormat.Default)]
		public List<AwardInfo> awardInfos
		{
			get
			{
				return this._awardInfos;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "resId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resId
		{
			get
			{
				return this._resId;
			}
			set
			{
				this._resId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
