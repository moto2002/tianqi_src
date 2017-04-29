using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1023), ForSend(1023), ProtoContract(Name = "UpdateEquipInfoChangeNty")]
	[Serializable]
	public class UpdateEquipInfoChangeNty : IExtensible
	{
		public static readonly short OP = 1023;

		private readonly List<EquipSimpleInfo> _equips = new List<EquipSimpleInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "equips", DataFormat = DataFormat.Default)]
		public List<EquipSimpleInfo> equips
		{
			get
			{
				return this._equips;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
