using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(774), ForSend(774), ProtoContract(Name = "DeleteEquipInfoNty")]
	[Serializable]
	public class DeleteEquipInfoNty : IExtensible
	{
		public static readonly short OP = 774;

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
