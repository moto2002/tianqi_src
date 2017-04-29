using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4325), ForSend(4325), ProtoContract(Name = "DrawReq")]
	[Serializable]
	public class DrawReq : IExtensible
	{
		public static readonly short OP = 4325;

		private int _drawId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "drawId", DataFormat = DataFormat.TwosComplement)]
		public int drawId
		{
			get
			{
				return this._drawId;
			}
			set
			{
				this._drawId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
