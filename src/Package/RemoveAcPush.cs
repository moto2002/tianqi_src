using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6391), ForSend(6391), ProtoContract(Name = "RemoveAcPush")]
	[Serializable]
	public class RemoveAcPush : IExtensible
	{
		public static readonly short OP = 6391;

		private int _typeId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
