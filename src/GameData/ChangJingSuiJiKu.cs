using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChangJingSuiJiKu")]
	[Serializable]
	public class ChangJingSuiJiKu : IExtensible
	{
		private int _sceneLibrary;

		private int _scene;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "sceneLibrary", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sceneLibrary
		{
			get
			{
				return this._sceneLibrary;
			}
			set
			{
				this._sceneLibrary = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "scene", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int scene
		{
			get
			{
				return this._scene;
			}
			set
			{
				this._scene = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
