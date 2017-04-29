using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4064), ForSend(4064), ProtoContract(Name = "PartnerAnswerRes")]
	[Serializable]
	public class PartnerAnswerRes : IExtensible
	{
		public static readonly short OP = 4064;

		private int _teamId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int teamId
		{
			get
			{
				return this._teamId;
			}
			set
			{
				this._teamId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
