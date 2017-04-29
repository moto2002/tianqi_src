using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5338), ForSend(5338), ProtoContract(Name = "SelectCareerRes")]
	[Serializable]
	public class SelectCareerRes : IExtensible
	{
		public static readonly short OP = 5338;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
