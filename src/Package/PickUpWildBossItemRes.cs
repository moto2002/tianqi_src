using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(629), ForSend(629), ProtoContract(Name = "PickUpWildBossItemRes")]
	[Serializable]
	public class PickUpWildBossItemRes : IExtensible
	{
		public static readonly short OP = 629;

		private readonly List<DropItem> _item = new List<DropItem>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "item", DataFormat = DataFormat.Default)]
		public List<DropItem> item
		{
			get
			{
				return this._item;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
