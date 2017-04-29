using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3210), ForSend(3210), ProtoContract(Name = "DefendFightReq")]
	[Serializable]
	public class DefendFightReq : IExtensible
	{
		public static readonly short OP = 3210;

		private int _dungeonId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dungeonId", DataFormat = DataFormat.TwosComplement)]
		public int dungeonId
		{
			get
			{
				return this._dungeonId;
			}
			set
			{
				this._dungeonId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
