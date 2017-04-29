using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4431), ForSend(4431), ProtoContract(Name = "GetLoginWelfareReq")]
	[Serializable]
	public class GetLoginWelfareReq : IExtensible
	{
		public static readonly short OP = 4431;

		private int _days;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "days", DataFormat = DataFormat.TwosComplement)]
		public int days
		{
			get
			{
				return this._days;
			}
			set
			{
				this._days = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
