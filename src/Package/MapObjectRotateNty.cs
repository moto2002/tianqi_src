using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(781), ForSend(781), ProtoContract(Name = "MapObjectRotateNty")]
	[Serializable]
	public class MapObjectRotateNty : IExtensible
	{
		public static readonly short OP = 781;

		private long _objId;

		private Vector2 _vector;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "objId", DataFormat = DataFormat.TwosComplement)]
		public long objId
		{
			get
			{
				return this._objId;
			}
			set
			{
				this._objId = value;
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
