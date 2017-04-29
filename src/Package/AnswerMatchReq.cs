using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(602), ForSend(602), ProtoContract(Name = "AnswerMatchReq")]
	[Serializable]
	public class AnswerMatchReq : IExtensible
	{
		public static readonly short OP = 602;

		private bool _stopMatching;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "stopMatching", DataFormat = DataFormat.Default)]
		public bool stopMatching
		{
			get
			{
				return this._stopMatching;
			}
			set
			{
				this._stopMatching = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
