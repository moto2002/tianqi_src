using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "SceneAppHeartBeatRes")]
	[Serializable]
	public class SceneAppHeartBeatRes : IExtensible
	{
		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
