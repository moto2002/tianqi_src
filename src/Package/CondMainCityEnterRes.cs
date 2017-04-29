using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(60), ForSend(60), ProtoContract(Name = "CondMainCityEnterRes")]
	[Serializable]
	public class CondMainCityEnterRes : IExtensible
	{
		public static readonly short OP = 60;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
