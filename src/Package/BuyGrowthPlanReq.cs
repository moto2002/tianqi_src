using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1711), ForSend(1711), ProtoContract(Name = "BuyGrowthPlanReq")]
	[Serializable]
	public class BuyGrowthPlanReq : IExtensible
	{
		public static readonly short OP = 1711;

		private int _typeId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
