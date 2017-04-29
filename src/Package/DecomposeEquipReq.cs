using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(400), ForSend(400), ProtoContract(Name = "DecomposeEquipReq")]
	[Serializable]
	public class DecomposeEquipReq : IExtensible
	{
		public static readonly short OP = 400;

		private readonly List<DecomposeEquipInfo> _decomposeEquipInfos = new List<DecomposeEquipInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "decomposeEquipInfos", DataFormat = DataFormat.Default)]
		public List<DecomposeEquipInfo> decomposeEquipInfos
		{
			get
			{
				return this._decomposeEquipInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
