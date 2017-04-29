using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3802), ForSend(3802), ProtoContract(Name = "RoleWillLeaveFieldNty")]
	[Serializable]
	public class RoleWillLeaveFieldNty : IExtensible
	{
		public static readonly short OP = 3802;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
