using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3843), ForSend(3843), ProtoContract(Name = "AdvancedOccupationNty")]
	[Serializable]
	public class AdvancedOccupationNty : IExtensible
	{
		public static readonly short OP = 3843;

		private int _advancedTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "advancedTimes", DataFormat = DataFormat.TwosComplement)]
		public int advancedTimes
		{
			get
			{
				return this._advancedTimes;
			}
			set
			{
				this._advancedTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
