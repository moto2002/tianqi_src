using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(753), ForSend(753), ProtoContract(Name = "DecomposeEquipRes")]
	[Serializable]
	public class DecomposeEquipRes : IExtensible
	{
		public static readonly short OP = 753;

		private readonly List<DecomposeItem> _decomposeItems = new List<DecomposeItem>();

		private readonly List<long> _decomposeEquipIds = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "decomposeItems", DataFormat = DataFormat.Default)]
		public List<DecomposeItem> decomposeItems
		{
			get
			{
				return this._decomposeItems;
			}
		}

		[ProtoMember(2, Name = "decomposeEquipIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> decomposeEquipIds
		{
			get
			{
				return this._decomposeEquipIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
