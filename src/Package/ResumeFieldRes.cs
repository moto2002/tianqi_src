using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(596), ForSend(596), ProtoContract(Name = "ResumeFieldRes")]
	[Serializable]
	public class ResumeFieldRes : IExtensible
	{
		public static readonly short OP = 596;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
