using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(654), ForSend(654), ProtoContract(Name = "ExploreBlockRes")]
	[Serializable]
	public class ExploreBlockRes : IExtensible
	{
		public static readonly short OP = 654;

		private readonly List<BlockInfo> _newOpenBlocks = new List<BlockInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "newOpenBlocks", DataFormat = DataFormat.Default)]
		public List<BlockInfo> newOpenBlocks
		{
			get
			{
				return this._newOpenBlocks;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
