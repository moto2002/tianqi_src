using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3819), ForSend(3819), ProtoContract(Name = "SyncPosToPy")]
	[Serializable]
	public class SyncPosToPy : IExtensible
	{
		public static readonly short OP = 3819;

		private long _objId;

		private Pos _toPos;

		private int _mapId;

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

		[ProtoMember(3, IsRequired = true, Name = "mapId", DataFormat = DataFormat.TwosComplement)]
		public int mapId
		{
			get
			{
				return this._mapId;
			}
			set
			{
				this._mapId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
