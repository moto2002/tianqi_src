using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "SceneAppHeartBeatNotice")]
	[Serializable]
	public class SceneAppHeartBeatNotice : IExtensible
	{
		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
