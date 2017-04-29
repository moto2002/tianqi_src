using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(679), ForSend(679), ProtoContract(Name = "StrongLoginPush")]
	[Serializable]
	public class StrongLoginPush : IExtensible
	{
		public static readonly short OP = 679;

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
