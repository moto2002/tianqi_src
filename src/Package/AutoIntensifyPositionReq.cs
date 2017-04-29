using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1118), ForSend(1118), ProtoContract(Name = "AutoIntensifyPositionReq")]
	[Serializable]
	public class AutoIntensifyPositionReq : IExtensible
	{
		public static readonly short OP = 1118;

		private int _position;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
