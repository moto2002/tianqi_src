using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(855), ForSend(855), ProtoContract(Name = "GetDailyTaskPrizeRes")]
	[Serializable]
	public class GetDailyTaskPrizeRes : IExtensible
	{
		public static readonly short OP = 855;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
