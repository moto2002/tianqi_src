using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(619), ForSend(619), ProtoContract(Name = "AcceptDebrisReq")]
	[Serializable]
	public class AcceptDebrisReq : IExtensible
	{
		public static readonly short OP = 619;

		private string _mineBlockId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "mineBlockId", DataFormat = DataFormat.Default)]
		public string mineBlockId
		{
			get
			{
				return this._mineBlockId;
			}
			set
			{
				this._mineBlockId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
