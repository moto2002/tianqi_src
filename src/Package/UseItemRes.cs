using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(423), ForSend(423), ProtoContract(Name = "UseItemRes")]
	[Serializable]
	public class UseItemRes : IExtensible
	{
		public static readonly short OP = 423;

		private long _id;

		private int _count;

		private int _itemId;

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

		[ProtoMember(2, IsRequired = true, Name = "count", DataFormat = DataFormat.ZigZag)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "itemId", DataFormat = DataFormat.ZigZag)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
