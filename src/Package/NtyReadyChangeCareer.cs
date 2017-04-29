using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3435), ForSend(3435), ProtoContract(Name = "NtyReadyChangeCareer")]
	[Serializable]
	public class NtyReadyChangeCareer : IExtensible
	{
		public static readonly short OP = 3435;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
