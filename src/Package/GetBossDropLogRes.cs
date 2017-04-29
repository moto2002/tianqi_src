using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4494), ForSend(4494), ProtoContract(Name = "GetBossDropLogRes")]
	[Serializable]
	public class GetBossDropLogRes : IExtensible
	{
		public static readonly short OP = 4494;

		private readonly List<BossDropLog> _bossDropLog = new List<BossDropLog>();

		private int _pageId;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "bossDropLog", DataFormat = DataFormat.Default)]
		public List<BossDropLog> bossDropLog
		{
			get
			{
				return this._bossDropLog;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "pageId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
