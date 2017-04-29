using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1278), ForSend(1278), ProtoContract(Name = "ContinueSecretAreaChallengeReq")]
	[Serializable]
	public class ContinueSecretAreaChallengeReq : IExtensible
	{
		public static readonly short OP = 1278;

		private bool _isContinue;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isContinue", DataFormat = DataFormat.Default)]
		public bool isContinue
		{
			get
			{
				return this._isContinue;
			}
			set
			{
				this._isContinue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
