using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "BuddyUpdateInfo")]
	[Serializable]
	public class BuddyUpdateInfo : IExtensible
	{
		private BuddyUpdateType.BUT _type;

		private readonly List<BuddyInfo> _buddies = new List<BuddyInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public BuddyUpdateType.BUT type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, Name = "buddies", DataFormat = DataFormat.Default)]
		public List<BuddyInfo> buddies
		{
			get
			{
				return this._buddies;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
