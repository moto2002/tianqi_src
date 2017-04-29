using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "SceneStartRes")]
	[Serializable]
	public class SceneStartRes : IExtensible
	{
		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
