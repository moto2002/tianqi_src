using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(106), ForSend(106), ProtoContract(Name = "EliteQuickEnterReq")]
	[Serializable]
	public class EliteQuickEnterReq : IExtensible
	{
		public static readonly short OP = 106;

		private int _copyId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "copyId", DataFormat = DataFormat.TwosComplement)]
		public int copyId
		{
			get
			{
				return this._copyId;
			}
			set
			{
				this._copyId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
