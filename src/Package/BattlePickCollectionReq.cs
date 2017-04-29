using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(990), ForSend(990), ProtoContract(Name = "BattlePickCollectionReq")]
	[Serializable]
	public class BattlePickCollectionReq : IExtensible
	{
		public static readonly short OP = 990;

		private int _pickIdx;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "pickIdx", DataFormat = DataFormat.TwosComplement)]
		public int pickIdx
		{
			get
			{
				return this._pickIdx;
			}
			set
			{
				this._pickIdx = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
