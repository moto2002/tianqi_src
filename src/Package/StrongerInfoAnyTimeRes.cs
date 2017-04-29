using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(968), ForSend(968), ProtoContract(Name = "StrongerInfoAnyTimeRes")]
	[Serializable]
	public class StrongerInfoAnyTimeRes : IExtensible
	{
		public static readonly short OP = 968;

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
