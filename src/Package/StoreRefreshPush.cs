using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(3982), ForSend(3982), ProtoContract(Name = "StoreRefreshPush")]
	[Serializable]
	public class StoreRefreshPush : IExtensible
	{
		public static readonly short OP = 3982;

		private readonly List<StoreRefreshInfo> _info = new List<StoreRefreshInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "info", DataFormat = DataFormat.Default)]
		public List<StoreRefreshInfo> info
		{
			get
			{
				return this._info;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
