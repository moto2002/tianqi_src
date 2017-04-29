using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7334), ForSend(7334), ProtoContract(Name = "BattleOperationReq")]
	[Serializable]
	public class BattleOperationReq : IExtensible
	{
		public static readonly short OP = 7334;

		private int _operationId;

		private int _dungeonType = 101;

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

		[ProtoMember(2, IsRequired = false, Name = "dungeonType", DataFormat = DataFormat.TwosComplement), DefaultValue(101)]
		public int dungeonType
		{
			get
			{
				return this._dungeonType;
			}
			set
			{
				this._dungeonType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
