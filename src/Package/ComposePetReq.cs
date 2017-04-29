using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(567), ForSend(567), ProtoContract(Name = "ComposePetReq")]
	[Serializable]
	public class ComposePetReq : IExtensible
	{
		public static readonly short OP = 567;

		private int _toPetId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "toPetId", DataFormat = DataFormat.TwosComplement)]
		public int toPetId
		{
			get
			{
				return this._toPetId;
			}
			set
			{
				this._toPetId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
