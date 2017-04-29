using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "InviteInfo")]
	[Serializable]
	public class InviteInfo : IExtensible
	{
		private InviteType.IMT _type;

		private long _id;

		private BuddyInfo _buddyInfo;

		private long _buildTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public InviteType.IMT type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "buddyInfo", DataFormat = DataFormat.Default)]
		public BuddyInfo buddyInfo
		{
			get
			{
				return this._buddyInfo;
			}
			set
			{
				this._buddyInfo = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "buildTime", DataFormat = DataFormat.TwosComplement)]
		public long buildTime
		{
			get
			{
				return this._buildTime;
			}
			set
			{
				this._buildTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
