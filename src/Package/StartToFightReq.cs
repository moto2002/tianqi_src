using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1795), ForSend(1795), ProtoContract(Name = "StartToFightReq")]
	[Serializable]
	public class StartToFightReq : IExtensible
	{
		public static readonly short OP = 1795;

		private string _blockId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "blockId", DataFormat = DataFormat.Default)]
		public string blockId
		{
			get
			{
				return this._blockId;
			}
			set
			{
				this._blockId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
