using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(860), ForSend(860), ProtoContract(Name = "RolePreciseMoveToReq")]
	[Serializable]
	public class RolePreciseMoveToReq : IExtensible
	{
		public static readonly short OP = 860;

		private Pos _toPos;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "toPos", DataFormat = DataFormat.Default)]
		public Pos toPos
		{
			get
			{
				return this._toPos;
			}
			set
			{
				this._toPos = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
