using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7321), ForSend(7321), ProtoContract(Name = "AdvancedOccupationLoginPush")]
	[Serializable]
	public class AdvancedOccupationLoginPush : IExtensible
	{
		public static readonly short OP = 7321;

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
