using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(669), ForSend(669), ProtoContract(Name = "SetFormationReq")]
	[Serializable]
	public class SetFormationReq : IExtensible
	{
		public static readonly short OP = 669;

		private PetFormation _formation;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "formation", DataFormat = DataFormat.Default)]
		public PetFormation formation
		{
			get
			{
				return this._formation;
			}
			set
			{
				this._formation = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
