using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3645), ForSend(3645), ProtoContract(Name = "KickedOffGuildNty")]
	[Serializable]
	public class KickedOffGuildNty : IExtensible
	{
		public static readonly short OP = 3645;

		private long _roleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
