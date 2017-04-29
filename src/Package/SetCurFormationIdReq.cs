using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7631), ForSend(7631), ProtoContract(Name = "SetCurFormationIdReq")]
	[Serializable]
	public class SetCurFormationIdReq : IExtensible
	{
		public static readonly short OP = 7631;

		private int _formationId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "formationId", DataFormat = DataFormat.TwosComplement)]
		public int formationId
		{
			get
			{
				return this._formationId;
			}
			set
			{
				this._formationId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
