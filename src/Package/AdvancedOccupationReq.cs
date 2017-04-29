using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2310), ForSend(2310), ProtoContract(Name = "AdvancedOccupationReq")]
	[Serializable]
	public class AdvancedOccupationReq : IExtensible
	{
		public static readonly short OP = 2310;

		private int _advancedStep;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "advancedStep", DataFormat = DataFormat.TwosComplement)]
		public int advancedStep
		{
			get
			{
				return this._advancedStep;
			}
			set
			{
				this._advancedStep = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
