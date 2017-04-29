using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(794), ForSend(794), ProtoContract(Name = "OwnerRedPacketsNty")]
	[Serializable]
	public class OwnerRedPacketsNty : IExtensible
	{
		public static readonly short OP = 794;

		private readonly List<RedPacketInfos> _redPackets = new List<RedPacketInfos>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "redPackets", DataFormat = DataFormat.Default)]
		public List<RedPacketInfos> redPackets
		{
			get
			{
				return this._redPackets;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
