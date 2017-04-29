using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(88), ForSend(88), ProtoContract(Name = "GetMemberInGuildWarReq")]
	[Serializable]
	public class GetMemberInGuildWarReq : IExtensible
	{
		public static readonly short OP = 88;

		private int _resourceId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "resourceId", DataFormat = DataFormat.TwosComplement)]
		public int resourceId
		{
			get
			{
				return this._resourceId;
			}
			set
			{
				this._resourceId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
