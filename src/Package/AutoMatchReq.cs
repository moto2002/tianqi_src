using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(505), ForSend(505), ProtoContract(Name = "AutoMatchReq")]
	[Serializable]
	public class AutoMatchReq : IExtensible
	{
		public static readonly short OP = 505;

		private AutoMatchType.ENUM _matchType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "matchType", DataFormat = DataFormat.TwosComplement)]
		public AutoMatchType.ENUM matchType
		{
			get
			{
				return this._matchType;
			}
			set
			{
				this._matchType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
