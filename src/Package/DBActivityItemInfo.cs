using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBActivityItemInfo")]
	[Serializable]
	public class DBActivityItemInfo : IExtensible
	{
		private int _typeId;

		private int _activityId;

		private readonly List<int> _roleIds = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "activityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activityId
		{
			get
			{
				return this._activityId;
			}
			set
			{
				this._activityId = value;
			}
		}

		[ProtoMember(3, Name = "roleIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> roleIds
		{
			get
			{
				return this._roleIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
