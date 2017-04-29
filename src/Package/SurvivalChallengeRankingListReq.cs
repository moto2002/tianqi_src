using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1377), ForSend(1377), ProtoContract(Name = "SurvivalChallengeRankingListReq")]
	[Serializable]
	public class SurvivalChallengeRankingListReq : IExtensible
	{
		public static readonly short OP = 1377;

		private int _page = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "page", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int page
		{
			get
			{
				return this._page;
			}
			set
			{
				this._page = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
