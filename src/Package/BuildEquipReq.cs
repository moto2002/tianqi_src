using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4830), ForSend(4830), ProtoContract(Name = "BuildEquipReq")]
	[Serializable]
	public class BuildEquipReq : IExtensible
	{
		public static readonly short OP = 4830;

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
