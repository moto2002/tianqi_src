using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6501), ForSend(6501), ProtoContract(Name = "UpFashionReq")]
	[Serializable]
	public class UpFashionReq : IExtensible
	{
		public static readonly short OP = 6501;

		private string _fashionId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "fashionId", DataFormat = DataFormat.Default)]
		public string fashionId
		{
			get
			{
				return this._fashionId;
			}
			set
			{
				this._fashionId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
