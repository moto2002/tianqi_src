using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2729), ForSend(2729), ProtoContract(Name = "InviteFriendRes")]
	[Serializable]
	public class InviteFriendRes : IExtensible
	{
		public static readonly short OP = 2729;

		private long _roleId;

		private int _cdTime;

		private int _mapId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "cdTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cdTime
		{
			get
			{
				return this._cdTime;
			}
			set
			{
				this._cdTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "mapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapId
		{
			get
			{
				return this._mapId;
			}
			set
			{
				this._mapId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
