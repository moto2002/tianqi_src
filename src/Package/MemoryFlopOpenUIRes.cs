using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(46), ForSend(46), ProtoContract(Name = "MemoryFlopOpenUIRes")]
	[Serializable]
	public class MemoryFlopOpenUIRes : IExtensible
	{
		public static readonly short OP = 46;

		private readonly List<MemoryFlopRankInfo> _rankInfo = new List<MemoryFlopRankInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "rankInfo", DataFormat = DataFormat.Default)]
		public List<MemoryFlopRankInfo> rankInfo
		{
			get
			{
				return this._rankInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
