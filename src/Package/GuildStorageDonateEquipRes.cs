using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(3868), ForSend(3868), ProtoContract(Name = "GuildStorageDonateEquipRes")]
	[Serializable]
	public class GuildStorageDonateEquipRes : IExtensible
	{
		public static readonly short OP = 3868;

		private readonly List<long> _donateEquipIds = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "donateEquipIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> donateEquipIds
		{
			get
			{
				return this._donateEquipIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
