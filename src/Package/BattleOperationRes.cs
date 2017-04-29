using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3335), ForSend(3335), ProtoContract(Name = "BattleOperationRes")]
	[Serializable]
	public class BattleOperationRes : IExtensible
	{
		public static readonly short OP = 3335;

		private int _operationId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "operationId", DataFormat = DataFormat.TwosComplement)]
		public int operationId
		{
			get
			{
				return this._operationId;
			}
			set
			{
				this._operationId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
