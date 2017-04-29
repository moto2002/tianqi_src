using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(692), ForSend(692), ProtoContract(Name = "BuyMopUpTimesReq")]
	[Serializable]
	public class BuyMopUpTimesReq : IExtensible
	{
		public static readonly short OP = 692;

		private int _dungeonId;

		private int _times = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dungeonId", DataFormat = DataFormat.TwosComplement)]
		public int dungeonId
		{
			get
			{
				return this._dungeonId;
			}
			set
			{
				this._dungeonId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "times", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
