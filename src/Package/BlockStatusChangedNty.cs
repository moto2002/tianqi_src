using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(913), ForSend(913), ProtoContract(Name = "BlockStatusChangedNty")]
	[Serializable]
	public class BlockStatusChangedNty : IExtensible
	{
		public static readonly short OP = 913;

		private readonly List<BlockInfo> _blockInfo = new List<BlockInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "blockInfo", DataFormat = DataFormat.Default)]
		public List<BlockInfo> blockInfo
		{
			get
			{
				return this._blockInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
