using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(5938), ForSend(5938), ProtoContract(Name = "SmeltEquipReq")]
	[Serializable]
	public class SmeltEquipReq : IExtensible
	{
		public static readonly short OP = 5938;

		private readonly List<DecomposeEquipInfo> _equipInfo = new List<DecomposeEquipInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "equipInfo", DataFormat = DataFormat.Default)]
		public List<DecomposeEquipInfo> equipInfo
		{
			get
			{
				return this._equipInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
