using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(8791), ForSend(8791), ProtoContract(Name = "DungeonPanelInfoPush")]
	[Serializable]
	public class DungeonPanelInfoPush : IExtensible
	{
		public static readonly short OP = 8791;

		private int _beGrabTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "beGrabTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int beGrabTimes
		{
			get
			{
				return this._beGrabTimes;
			}
			set
			{
				this._beGrabTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
