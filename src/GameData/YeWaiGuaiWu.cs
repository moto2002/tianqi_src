using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YeWaiGuaiWu")]
	[Serializable]
	public class YeWaiGuaiWu : IExtensible
	{
		private int _id;

		private int _sceneId;

		private int _monsterRefreshId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "sceneId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sceneId
		{
			get
			{
				return this._sceneId;
			}
			set
			{
				this._sceneId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "monsterRefreshId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterRefreshId
		{
			get
			{
				return this._monsterRefreshId;
			}
			set
			{
				this._monsterRefreshId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
