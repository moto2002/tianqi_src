using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6642), ForSend(6642), ProtoContract(Name = "DownLoadFinishRes")]
	[Serializable]
	public class DownLoadFinishRes : IExtensible
	{
		public static readonly short OP = 6642;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
