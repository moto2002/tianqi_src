using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6829), ForSend(6829), ProtoContract(Name = "BountyMatchStatusNty")]
	[Serializable]
	public class BountyMatchStatusNty : IExtensible
	{
		public static readonly short OP = 6829;

		private int _dungeonId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "dungeonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
