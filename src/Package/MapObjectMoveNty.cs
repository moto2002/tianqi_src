using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(683), ForSend(683), ProtoContract(Name = "MapObjectMoveNty")]
	[Serializable]
	public class MapObjectMoveNty : IExtensible
	{
		public static readonly short OP = 683;

		private long _objId;

		private Pos _toPos;

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

		[ProtoMember(2, IsRequired = true, Name = "toPos", DataFormat = DataFormat.Default)]
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
