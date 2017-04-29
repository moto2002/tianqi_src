using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(673), ForSend(673), ProtoContract(Name = "EquipAdvancedReq")]
	[Serializable]
	public class EquipAdvancedReq : IExtensible
	{
		public static readonly short OP = 673;

		private int _position;

		private long _targetEquipId;

		private readonly List<long> _sourceEquipIds = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "targetEquipId", DataFormat = DataFormat.TwosComplement)]
		public long targetEquipId
		{
			get
			{
				return this._targetEquipId;
			}
			set
			{
				this._targetEquipId = value;
			}
		}

		[ProtoMember(3, Name = "sourceEquipIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> sourceEquipIds
		{
			get
			{
				return this._sourceEquipIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
