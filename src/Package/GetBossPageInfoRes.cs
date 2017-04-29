using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(4202), ForSend(4202), ProtoContract(Name = "GetBossPageInfoRes")]
	[Serializable]
	public class GetBossPageInfoRes : IExtensible
	{
		public static readonly short OP = 4202;

		private int _pageId;

		private readonly List<BossLabelInfo> _bossLabelInfo = new List<BossLabelInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "pageId", DataFormat = DataFormat.TwosComplement)]
		public int pageId
		{
			get
			{
				return this._pageId;
			}
			set
			{
				this._pageId = value;
			}
		}

		[ProtoMember(2, Name = "bossLabelInfo", DataFormat = DataFormat.Default)]
		public List<BossLabelInfo> bossLabelInfo
		{
			get
			{
				return this._bossLabelInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
