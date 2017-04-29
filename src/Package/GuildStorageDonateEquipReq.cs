using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(3867), ForSend(3867), ProtoContract(Name = "GuildStorageDonateEquipReq")]
	[Serializable]
	public class GuildStorageDonateEquipReq : IExtensible
	{
		public static readonly short OP = 3867;

		private readonly List<EquipBriefInfo> _equips = new List<EquipBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "equips", DataFormat = DataFormat.Default)]
		public List<EquipBriefInfo> equips
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
