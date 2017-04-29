using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1030), ForSend(1030), ProtoContract(Name = "ExitFromGangFightFieldRes")]
	[Serializable]
	public class ExitFromGangFightFieldRes : IExtensible
	{
		public static readonly short OP = 1030;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
