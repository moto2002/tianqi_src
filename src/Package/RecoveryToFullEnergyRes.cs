using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1029), ForSend(1029), ProtoContract(Name = "RecoveryToFullEnergyRes")]
	[Serializable]
	public class RecoveryToFullEnergyRes : IExtensible
	{
		public static readonly short OP = 1029;

		private int _purchaseNum;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "purchaseNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int purchaseNum
		{
			get
			{
				return this._purchaseNum;
			}
			set
			{
				this._purchaseNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
