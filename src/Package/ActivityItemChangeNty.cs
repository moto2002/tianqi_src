using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(922), ForSend(922), ProtoContract(Name = "ActivityItemChangeNty")]
	[Serializable]
	public class ActivityItemChangeNty : IExtensible
	{
		[ProtoContract(Name = "UpdateType")]
		[Serializable]
		public class UpdateType : IExtensible
		{
			[ProtoContract(Name = "UDT")]
			public enum UDT
			{
				[ProtoEnum(Name = "Add", Value = 0)]
				Add,
				[ProtoEnum(Name = "Del", Value = 1)]
				Del,
				[ProtoEnum(Name = "Update", Value = 3)]
				Update = 3
			}

			private IExtension extensionObject;

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 922;

		private readonly List<ActivityItemInfo> _activitiesInfo = new List<ActivityItemInfo>();

		private ActivityItemChangeNty.UpdateType.UDT _updateType;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "activitiesInfo", DataFormat = DataFormat.Default)]
		public List<ActivityItemInfo> activitiesInfo
		{
			get
			{
				return this._activitiesInfo;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "updateType", DataFormat = DataFormat.TwosComplement)]
		public ActivityItemChangeNty.UpdateType.UDT updateType
		{
			get
			{
				return this._updateType;
			}
			set
			{
				this._updateType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
