using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4042), ForSend(4042), ProtoContract(Name = "TeamStartFightAnswerReq")]
	[Serializable]
	public class TeamStartFightAnswerReq : IExtensible
	{
		public static readonly short OP = 4042;

		private bool _agree;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "agree", DataFormat = DataFormat.Default)]
		public bool agree
		{
			get
			{
				return this._agree;
			}
			set
			{
				this._agree = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
