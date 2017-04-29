using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4901), ForSend(4901), ProtoContract(Name = "PartnerRejectNty")]
	[Serializable]
	public class PartnerRejectNty : IExtensible
	{
		public static readonly short OP = 4901;

		private string _roleName;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleName", DataFormat = DataFormat.Default)]
		public string roleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
