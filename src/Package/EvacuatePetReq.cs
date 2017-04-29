using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1790), ForSend(1790), ProtoContract(Name = "EvacuatePetReq")]
	[Serializable]
	public class EvacuatePetReq : IExtensible
	{
		public static readonly short OP = 1790;

		private string _mineBlockId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "mineBlockId", DataFormat = DataFormat.Default)]
		public string mineBlockId
		{
			get
			{
				return this._mineBlockId;
			}
			set
			{
				this._mineBlockId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
