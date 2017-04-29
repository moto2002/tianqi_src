using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(4993), ForSend(4993), ProtoContract(Name = "StoreLoginPush")]
	[Serializable]
	public class StoreLoginPush : IExtensible
	{
		public static readonly short OP = 4993;

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
