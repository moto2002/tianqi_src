using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1456), ForSend(1456), ProtoContract(Name = "UpGodWeaponLvRes")]
	[Serializable]
	public class UpGodWeaponLvRes : IExtensible
	{
		public static readonly short OP = 1456;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
