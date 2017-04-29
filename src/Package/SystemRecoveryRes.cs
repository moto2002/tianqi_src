using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(5782), ForSend(5782), ProtoContract(Name = "SystemRecoveryRes")]
	[Serializable]
	public class SystemRecoveryRes : IExtensible
	{
		public static readonly short OP = 5782;

		private long _id;

		private readonly List<ItemBriefInfo> _itemInfo = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, Name = "itemInfo", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> itemInfo
		{
			get
			{
				return this._itemInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
