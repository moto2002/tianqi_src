using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(569), ForSend(569), ProtoContract(Name = "RoleRotateReq")]
	[Serializable]
	public class RoleRotateReq : IExtensible
	{
		public static readonly short OP = 569;

		private Vector2 _vector;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "vector", DataFormat = DataFormat.Default)]
		public Vector2 vector
		{
			get
			{
				return this._vector;
			}
			set
			{
				this._vector = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
