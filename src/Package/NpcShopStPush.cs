using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1547), ForSend(1547), ProtoContract(Name = "NpcShopStPush")]
	[Serializable]
	public class NpcShopStPush : IExtensible
	{
		public static readonly short OP = 1547;

		private readonly List<NpcShopSt> _npcShopSt = new List<NpcShopSt>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "npcShopSt", DataFormat = DataFormat.Default)]
		public List<NpcShopSt> npcShopSt
		{
			get
			{
				return this._npcShopSt;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
