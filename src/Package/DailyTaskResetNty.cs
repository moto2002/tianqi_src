using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6731), ForSend(6731), ProtoContract(Name = "DailyTaskResetNty")]
	[Serializable]
	public class DailyTaskResetNty : IExtensible
	{
		public static readonly short OP = 6731;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
