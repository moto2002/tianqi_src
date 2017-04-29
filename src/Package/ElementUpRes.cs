using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(516), ForSend(516), ProtoContract(Name = "ElementUpRes")]
	[Serializable]
	public class ElementUpRes : IExtensible
	{
		public static readonly short OP = 516;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
