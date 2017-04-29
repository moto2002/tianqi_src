using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(3869), ForSend(3869), ProtoContract(Name = "GuildStorageDecomposeEquipReq")]
	[Serializable]
	public class GuildStorageDecomposeEquipReq : IExtensible
	{
		public static readonly short OP = 3869;

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
