using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(96), ForSend(96), ProtoContract(Name = "EnterGuildBattleRes")]
	[Serializable]
	public class EnterGuildBattleRes : IExtensible
	{
		public static readonly short OP = 96;

		private int _battleNo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "battleNo", DataFormat = DataFormat.TwosComplement)]
		public int battleNo
		{
			get
			{
				return this._battleNo;
			}
			set
			{
				this._battleNo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
