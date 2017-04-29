using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2006), ForSend(2006), ProtoContract(Name = "ExtendMemoryFlopTimesRes")]
	[Serializable]
	public class ExtendMemoryFlopTimesRes : IExtensible
	{
		public static readonly short OP = 2006;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
