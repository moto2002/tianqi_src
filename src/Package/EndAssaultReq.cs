using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(556), ForSend(556), ProtoContract(Name = "EndAssaultReq")]
	[Serializable]
	public class EndAssaultReq : IExtensible
	{
		public static readonly short OP = 556;

		private Pos _pos;

		private Vector2 _vector;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
		public Pos pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "vector", DataFormat = DataFormat.Default)]
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
