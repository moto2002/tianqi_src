using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(677), ForSend(677), ProtoContract(Name = "StrongerInfoRes")]
	[Serializable]
	public class StrongerInfoRes : IExtensible
	{
		public static readonly short OP = 677;

		private readonly List<StrongerInfo> _strongerInfos = new List<StrongerInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "strongerInfos", DataFormat = DataFormat.Default)]
		public List<StrongerInfo> strongerInfos
		{
			get
			{
				return this._strongerInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
