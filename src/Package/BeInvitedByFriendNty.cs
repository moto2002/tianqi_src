using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4832), ForSend(4832), ProtoContract(Name = "BeInvitedByFriendNty")]
	[Serializable]
	public class BeInvitedByFriendNty : IExtensible
	{
		public static readonly short OP = 4832;

		private string _friendName;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "friendName", DataFormat = DataFormat.Default)]
		public string friendName
		{
			get
			{
				return this._friendName;
			}
			set
			{
				this._friendName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
