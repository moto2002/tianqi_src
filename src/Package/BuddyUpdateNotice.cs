using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(737), ForSend(737), ProtoContract(Name = "BuddyUpdateNotice")]
	[Serializable]
	public class BuddyUpdateNotice : IExtensible
	{
		public static readonly short OP = 737;

		private readonly List<BuddyUpdateInfo> _infos = new List<BuddyUpdateInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infos", DataFormat = DataFormat.Default)]
		public List<BuddyUpdateInfo> infos
		{
			get
			{
				return this._infos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
