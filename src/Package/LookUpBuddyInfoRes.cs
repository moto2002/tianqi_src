using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(763), ForSend(763), ProtoContract(Name = "LookUpBuddyInfoRes")]
	[Serializable]
	public class LookUpBuddyInfoRes : IExtensible
	{
		public static readonly short OP = 763;

		private readonly List<BuddyInfo> _buddyInfos = new List<BuddyInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "buddyInfos", DataFormat = DataFormat.Default)]
		public List<BuddyInfo> buddyInfos
		{
			get
			{
				return this._buddyInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
