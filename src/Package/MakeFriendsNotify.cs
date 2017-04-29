using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(742), ProtoContract(Name = "MakeFriendsNotify")]
	[Serializable]
	public class MakeFriendsNotify : IExtensible
	{
		public static readonly short OP = 742;

		private BuddyInfo _info;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "info", DataFormat = DataFormat.Default)]
		public BuddyInfo info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
