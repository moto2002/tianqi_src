using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(435), ForSend(435), ProtoContract(Name = "BuddyInfos")]
	[Serializable]
	public class BuddyInfos : IExtensible
	{
		public static readonly short OP = 435;

		private readonly List<BuddyInfo> _buddies = new List<BuddyInfo>();

		private readonly List<BuddyInfo> _applicants = new List<BuddyInfo>();

		private readonly List<BuddyInfo> _blackLists = new List<BuddyInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "buddies", DataFormat = DataFormat.Default)]
		public List<BuddyInfo> buddies
		{
			get
			{
				return this._buddies;
			}
		}

		[ProtoMember(2, Name = "applicants", DataFormat = DataFormat.Default)]
		public List<BuddyInfo> applicants
		{
			get
			{
				return this._applicants;
			}
		}

		[ProtoMember(3, Name = "blackLists", DataFormat = DataFormat.Default)]
		public List<BuddyInfo> blackLists
		{
			get
			{
				return this._blackLists;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
