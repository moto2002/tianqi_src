using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1321), ForSend(1321), ProtoContract(Name = "SurvivalChallengeGetBaseInfoRes")]
	[Serializable]
	public class SurvivalChallengeGetBaseInfoRes : IExtensible
	{
		public static readonly short OP = 1321;

		private SurvivalChallengeGetBaseInfoNty _info;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "info", DataFormat = DataFormat.Default)]
		public SurvivalChallengeGetBaseInfoNty info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
