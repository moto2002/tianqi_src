using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BroadcastType")]
	[Serializable]
	public class BroadcastType : IExtensible
	{
		[ProtoContract(Name = "Type")]
		public enum Type
		{
			[ProtoEnum(Name = "Title", Value = 1)]
			Title = 1,
			[ProtoEnum(Name = "Guild", Value = 2)]
			Guild
		}

		public static readonly short OP = 576;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
