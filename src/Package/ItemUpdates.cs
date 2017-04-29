using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(486), ForSend(486), ProtoContract(Name = "ItemUpdates")]
	[Serializable]
	public class ItemUpdates : IExtensible
	{
		public static readonly short OP = 486;

		private readonly List<ItemUpdate> _updates = new List<ItemUpdate>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "updates", DataFormat = DataFormat.Default)]
		public List<ItemUpdate> updates
		{
			get
			{
				return this._updates;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
