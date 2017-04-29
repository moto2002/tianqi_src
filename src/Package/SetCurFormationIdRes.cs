using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1297), ForSend(1297), ProtoContract(Name = "SetCurFormationIdRes")]
	[Serializable]
	public class SetCurFormationIdRes : IExtensible
	{
		public static readonly short OP = 1297;

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
