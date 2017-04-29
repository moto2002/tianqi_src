using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "Friend")]
	[Serializable]
	public class Friend : IExtensible
	{
		private long _roleId;

		private FriendRelation.FR _relation;

		private int _online;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "relation", DataFormat = DataFormat.TwosComplement), DefaultValue(FriendRelation.FR.Buddy)]
		public FriendRelation.FR relation
		{
			get
			{
				return this._relation;
			}
			set
			{
				this._relation = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "online", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int online
		{
			get
			{
				return this._online;
			}
			set
			{
				this._online = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
