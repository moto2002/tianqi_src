using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1807), ForSend(1807), ProtoContract(Name = "LastBlockChangedNty")]
	[Serializable]
	public class LastBlockChangedNty : IExtensible
	{
		public static readonly short OP = 1807;

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
