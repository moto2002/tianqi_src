using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4080), ForSend(4080), ProtoContract(Name = "LeaderProcessAppointReq")]
	[Serializable]
	public class LeaderProcessAppointReq : IExtensible
	{
		public static readonly short OP = 4080;

		private long _roleId;

		private bool _agree;

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

		[ProtoMember(2, IsRequired = true, Name = "agree", DataFormat = DataFormat.Default)]
		public bool agree
		{
			get
			{
				return this._agree;
			}
			set
			{
				this._agree = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
