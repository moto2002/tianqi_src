using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"scenceId"
	}), ProtoContract(Name = "Server")]
	[Serializable]
	public class Server : IExtensible
	{
		private int _scenceId;

		private int _serverNameId;

		private int _picName;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "scenceId", DataFormat = DataFormat.TwosComplement)]
		public int scenceId
		{
			get
			{
				return this._scenceId;
			}
			set
			{
				this._scenceId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "serverNameId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int serverNameId
		{
			get
			{
				return this._serverNameId;
			}
			set
			{
				this._serverNameId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "picName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int picName
		{
			get
			{
				return this._picName;
			}
			set
			{
				this._picName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
