using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(232), ForSend(232), ProtoContract(Name = "OccupyPointNty")]
	[Serializable]
	public class OccupyPointNty : IExtensible
	{
		public static readonly short OP = 232;

		private long _guildId;

		private int _pointId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "guildId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this._guildId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "pointId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pointId
		{
			get
			{
				return this._pointId;
			}
			set
			{
				this._pointId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
