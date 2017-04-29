using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(894), ForSend(894), ProtoContract(Name = "DungeonMiscChangedNty")]
	[Serializable]
	public class DungeonMiscChangedNty : IExtensible
	{
		public static readonly short OP = 894;

		private int _usedFreeMopUpTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "usedFreeMopUpTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int usedFreeMopUpTimes
		{
			get
			{
				return this._usedFreeMopUpTimes;
			}
			set
			{
				this._usedFreeMopUpTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
