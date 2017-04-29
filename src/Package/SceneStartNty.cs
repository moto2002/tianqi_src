using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "SceneStartNty")]
	[Serializable]
	public class SceneStartNty : IExtensible
	{
		private SceneServerInfo _serverInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "serverInfo", DataFormat = DataFormat.Default)]
		public SceneServerInfo serverInfo
		{
			get
			{
				return this._serverInfo;
			}
			set
			{
				this._serverInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
