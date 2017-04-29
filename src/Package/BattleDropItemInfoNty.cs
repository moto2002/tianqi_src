using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(905), ForSend(905), ProtoContract(Name = "BattleDropItemInfoNty")]
	[Serializable]
	public class BattleDropItemInfoNty : IExtensible
	{
		[ProtoContract(Name = "DropItemInfo")]
		[Serializable]
		public class DropItemInfo : IExtensible
		{
			private DropItem _dropItems;

			private int _operatorType = 1;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "dropItems", DataFormat = DataFormat.Default)]
			public DropItem dropItems
			{
				get
				{
					return this._dropItems;
				}
				set
				{
					this._dropItems = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "operatorType", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
			public int operatorType
			{
				get
				{
					return this._operatorType;
				}
				set
				{
					this._operatorType = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 905;

		private readonly List<BattleDropItemInfoNty.DropItemInfo> _dropItemInfo = new List<BattleDropItemInfoNty.DropItemInfo>();

		private long _roleId;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "dropItemInfo", DataFormat = DataFormat.Default)]
		public List<BattleDropItemInfoNty.DropItemInfo> dropItemInfo
		{
			get
			{
				return this._dropItemInfo;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "roleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
