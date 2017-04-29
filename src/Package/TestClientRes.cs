using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1153), ForSend(1153), ProtoContract(Name = "TestClientRes")]
	[Serializable]
	public class TestClientRes : IExtensible
	{
		[ProtoContract(Name = "ItemInfo")]
		[Serializable]
		public class ItemInfo : IExtensible
		{
			private int _id;

			private int _count;

			private string _desc;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
			public int id
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

			[ProtoMember(2, IsRequired = true, Name = "count", DataFormat = DataFormat.TwosComplement)]
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

			[ProtoMember(3, IsRequired = true, Name = "desc", DataFormat = DataFormat.Default)]
			public string desc
			{
				get
				{
					return this._desc;
				}
				set
				{
					this._desc = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 1153;

		private readonly List<TestClientRes.ItemInfo> _items = new List<TestClientRes.ItemInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TestClientRes.ItemInfo> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
