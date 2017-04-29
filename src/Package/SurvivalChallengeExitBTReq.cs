using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1111), ForSend(1111), ProtoContract(Name = "SurvivalChallengeExitBTReq")]
	[Serializable]
	public class SurvivalChallengeExitBTReq : IExtensible
	{
		public static readonly short OP = 1111;

		private bool _again;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "again", DataFormat = DataFormat.Default)]
		public bool again
		{
			get
			{
				return this._again;
			}
			set
			{
				this._again = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
