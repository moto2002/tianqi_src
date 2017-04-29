using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(148), ForSend(148), ProtoContract(Name = "GetExperienceCopyRewardRes")]
	[Serializable]
	public class GetExperienceCopyRewardRes : IExtensible
	{
		public static readonly short OP = 148;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
