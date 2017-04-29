using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2103), ForSend(2103), ProtoContract(Name = "DarkTrainQuickEnterRes")]
	[Serializable]
	public class DarkTrainQuickEnterRes : IExtensible
	{
		public static readonly short OP = 2103;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
