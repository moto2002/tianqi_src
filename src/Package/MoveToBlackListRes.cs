using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(752), ForSend(752), ProtoContract(Name = "MoveToBlackListRes")]
	[Serializable]
	public class MoveToBlackListRes : IExtensible
	{
		public static readonly short OP = 752;

		private long _id;

		private BuddyRelation.BR _relation = BuddyRelation.BR.Stranger;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "relation", DataFormat = DataFormat.TwosComplement), DefaultValue(BuddyRelation.BR.Stranger)]
		public BuddyRelation.BR relation
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
